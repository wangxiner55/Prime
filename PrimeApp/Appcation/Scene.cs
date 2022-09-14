using PrimeApp.Comman;
using PrimeApp.Components;
using PrimeApp.Ultites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrimeApp.Appcation
{
	[DataContract]
	public class Scene : ViewModeBase
	{

		// *********************** Member ********************* //



		private string _name;

		private bool _isActive;

		private readonly ObservableCollection<GameEntity> _gameEntities = new ObservableCollection<GameEntity>();



		// *********************** Instance ********************* //



		public Scene(Project project, string name)
		{
			Debug.Assert(project != null);
			Project = project;
			Name = name;
			OnDeserialized(new StreamingContext());
		}



		// *********************** Command ********************* //



		public ICommand AddGameEntityCommand { get; private set; }
		public ICommand RemoveGameEntityCommand { get; private set; }



		// *********************** Function ********************* //



		[DataMember(Name = nameof(GameEntities))]
		public ReadOnlyObservableCollection<GameEntity> GameEntities { get; private set; }


		[DataMember]
		public string Name
		{
			get { return _name; }
			set { 
				if(_name != value)
				{
					_name = value;
					OnPropertyChanged();
				}
				 
			}
		}


		[DataMember]
		public Project Project { get;private set; }


		[DataMember]
		public bool IsActive
		{
			get => _isActive;
			set
			{
				if(_isActive != value)
				{
					_isActive = value;
					OnPropertyChanged();
				}
			}
		}


		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
			//if (_gameEntities == null) _gameEntities = new ObservableCollection<GameEntity>();
			if (_gameEntities != null)
			{
				GameEntities = new ReadOnlyObservableCollection<GameEntity>(_gameEntities);
				OnPropertyChanged(nameof(GameEntities));
			}

			AddGameEntityCommand = new RelayCommand<GameEntity>(x =>
			{

				AddGameEntity(x);
				var gameEntityIndex = _gameEntities.Count - 1;
				Project.UndoRedo.Add(new UndoRedoAction(
					() => RemoveGameEntity(x),
					() => _gameEntities.Insert(gameEntityIndex, x),
					$"Add{x.Name} to {Name}"));
			});

			RemoveGameEntityCommand = new RelayCommand<GameEntity>(x =>
			{
				var gameEntityIndex = _gameEntities.IndexOf(x);
				RemoveGameEntity(x);
				Project.UndoRedo.Add(new UndoRedoAction(
					() => _gameEntities.Insert(gameEntityIndex, x),
					() => RemoveGameEntity(x),
					$"Remove{x.Name} from {Name}"));
			});
		}


		private void AddGameEntity(GameEntity gameEntity)
		{
			Debug.Assert(!_gameEntities.Contains(gameEntity));
			_gameEntities.Add(gameEntity);

		}
		

		private void RemoveGameEntity(GameEntity gameEntity)
		{
			//Debug.Assert(!_gameEntities.Contains(gameEntity));
			//Bug -- 会自动在这里断点


			_gameEntities.Remove(gameEntity);


			
		}



		// End
	}
}
