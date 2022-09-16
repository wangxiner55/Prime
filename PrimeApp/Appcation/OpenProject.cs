﻿using PrimeApp.Ultites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Appcation
{



	[DataContract]
	public class ProjectData
	{

		// ******************** Member *********************** //
		[DataMember]
		public string ProjectName { get; set; }

		[DataMember]
		public string ProjectPath { get; set; }

		[DataMember]
		public DateTime Data { get; set; }

		public string FullPath { get => $"{ProjectPath}{ProjectName}{Project.Extension}"; }

		public byte[] Icon { get; set; }

		public byte[] ScreenShot { get; set; }

		//Class End
		
	}

	[DataContract]
	public class ProjectDataList
	{

		// ******************** Member *********************** //
		[DataMember]
		public List<ProjectData> Projects { get; set; }


		// Class End
	}

	public class OpenProject
	{


		// ******************** Member *********************** //


		private static readonly string _applicationDataPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\PrimalEditor\";
		private static readonly string _projectDataPath;
		private static readonly ObservableCollection<ProjectData> _projects = new ObservableCollection<ProjectData>();
		public static ReadOnlyObservableCollection<ProjectData> Projects { get; }



		// ******************** Instance *********************** //


		static OpenProject()
		{
			try
			{
				if (!Directory.Exists(_applicationDataPath)) Directory.CreateDirectory(_applicationDataPath);
				_projectDataPath = $@"{_applicationDataPath}ProjectData.xml";
				Projects = new ReadOnlyObservableCollection<ProjectData>(_projects);
				ReadProjectData();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);

			}
		}


		// ******************** Function *********************** //



		private static void WriteProjectData()
		{
			var projects = _projects.OrderBy(x => x.Data).ToList();
			Serializer.WriteToFile(new ProjectDataList { Projects = projects }, _projectDataPath);

		}


		public static Project Open(ProjectData data)
		{
			ReadProjectData();

			var project = _projects.FirstOrDefault(x => x.FullPath == data.FullPath);

			if(project != null)
			{
				project.Data = DateTime.Now;
			}
			else
			{
				project = data;
				project.Data = DateTime.Now;
				_projects.Add(project);

			}

			WriteProjectData();

			return Project.Load(project.FullPath);

		}


		private static void ReadProjectData()
		{
			if (File.Exists(_projectDataPath))
			{
				var projects = Serializer.ReadFromFile<ProjectDataList>(_projectDataPath).Projects.OrderByDescending(x => x.Data);
				_projects.Clear();
				foreach (var project in projects)
				{
					if (File.Exists(project.FullPath))
					{
						project.Icon = File.ReadAllBytes($@"{project.ProjectPath}\.Primal\Icon.png");
						project.ScreenShot = File.ReadAllBytes($@"{project.ProjectPath}\.Primal\ScreenShot.png");
						//_projects.Add(project);

					}
				}
			}
		}

		// Class End

	}




}

