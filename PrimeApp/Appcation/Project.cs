﻿
using PrimeApp.Comman;
using PrimeApp.Ultites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace PrimeApp.Appcation
{
	[DataContract(Name ="Game")]
	public class Project : ViewModeBase
	{

		public static string Extension { get; } = ".Primal";
		[DataMember]
		public string Name { get; private set; } = "New Project";
		[DataMember]
		public string Path { get; private set; }

		public string FullPath => $"{Path}{Name}{Extension}";
		[DataMember(Name ="Scenes")]
		private ObservableCollection<Scene> _scenes = new ObservableCollection<Scene>();

		public ReadOnlyObservableCollection<Scene> Scenes { get; private set; }

		private Scene _activeScene;

		[DataMember]
		public Scene ActiveScene
		{
			get => _activeScene;

			set
			{
				if(_activeScene != value)
				{
					_activeScene = value;
					OnPropertyChanged(nameof(ActiveScene));
				}
			}
		}

		public static Project Current => Application.Current.MainWindow.DataContext as Project;

		public static UndoRedo UndoRedo { get; } = new UndoRedo();

		public ICommand RemoveScene { get; private set; }
		public ICommand AddScene { get; private set; }
		
		public ICommand Undo { get; private set; }
		public ICommand Redo { get; private set; }


		private void AddSceneInternal(string SceneName)
		{
			Debug.Assert(!string.IsNullOrEmpty(SceneName.Trim()));
			_scenes.Add(new Scene(this,SceneName));
		}

		private void RemoveSceneInternal(Scene scene)
		{
			Debug.Assert(_scenes.Contains(scene));
			_scenes.Remove(scene);
		}

		public void unLoad()
		{

		}


		public static Project Load(string file)
		{
			Debug.Assert(File.Exists(file));
			return Serializer.ReadFromFile<Project>(file);
		}

		public static void Save(Project project)
		{
			Serializer.WriteToFile(project, project.FullPath);
		}


		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			if(_scenes != null)
			{
				Scenes = new ReadOnlyObservableCollection<Scene>(_scenes);
				OnPropertyChanged(nameof(Scenes));
			}
			ActiveScene = Scenes.FirstOrDefault(x => x.IsActive);

			AddScene = new RelayCommand<object>(x =>
			{
				AddSceneInternal($"New Scene{_scenes.Count}");
				var newScene = _scenes.Last();
				var sceneIndex = _scenes.Count - 1;
				UndoRedo.Add(new UndoRedoAction(
					() => RemoveSceneInternal(newScene),
					() => _scenes.Insert(sceneIndex,newScene),
					$"Add{newScene.Name}"));
			});


			RemoveScene = new RelayCommand<Scene>(x =>
			{
				var sceneIndex = _scenes.IndexOf(x);
				RemoveSceneInternal(x);
				UndoRedo.Add(new UndoRedoAction(
					() => _scenes.Insert(sceneIndex, x),
					() => RemoveSceneInternal(x),
					$"Remove{x.Name}"));
			}, x => !x.IsActive);

			Undo = new RelayCommand<object>(x => UndoRedo.Undo());
			Redo = new RelayCommand<object>(x => UndoRedo.Redo());

		}

		public Project(string name , string path)
		{
			Name = name;
			Path = path;

			OnDeserialized(new StreamingContext());	

		}



	}

	


}