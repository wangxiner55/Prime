//Newprojets 为项目代理 NewProject为项目新建页面



using ImTools;
using PrimeApp.Comman;
using PrimeApp.Ultites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace PrimeApp.Appcation
{

	

	[DataContract]
	public class ProjectTemple
	{

		// ************************* Member *********************** //



		[DataMember]
		public string ProjectType { get; set; }
		[DataMember]
		public string ProjectFile { get; set; }
		[DataMember]
		public List<string> Floders { get; set; }


		public byte[] Icon { get; set; }
		public byte[] ScreenShot { get; set; }

		public string IconFilePath { get; set; }
		public string ScreenShotFilePath { get; set; }
		public string ProjectFilePath { get; set; }
	}




	public class NewProjects : ViewModeBase
	{


		// ************************* Member *********************** //



		private static int g_count;

		private string _projectPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\EEngine\EngineProject";

		private string _projectName = "NewProjects";

		private readonly string _templatePath = @"..\..\PrimeEditer\ProjectTemplates";

		private bool _isValid;

		private string _errorMsg;

		private readonly ObservableCollection<ProjectTemple> _projectTemplates = new ObservableCollection<ProjectTemple>();



		// ********************** Instance ***************************** //



		public NewProjects()
		{
			ProjectTemplates = new ReadOnlyObservableCollection<ProjectTemple>(_projectTemplates);
			
			try
			{
				var templateFiles = Directory.GetFiles(_templatePath, "Template.xml", SearchOption.AllDirectories);
				Debug.Assert(templateFiles.Any());
				foreach (var file in templateFiles)
				{

					var template = Serializer.ReadFromFile<ProjectTemple>(file);
					template.IconFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "Icon.png"));
					template.Icon = File.ReadAllBytes(template.IconFilePath);
					template.ScreenShotFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "ScreenShot.png"));
					template.ScreenShot = File.ReadAllBytes(template.ScreenShotFilePath);
					template.ProjectFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), template.ProjectFile));

					_projectTemplates.Add(template);
				}
				ValidaProjectPath();

			}
			catch (Exception ex)
			{
				Debug.Write(ex.Message);
			}
		}



		// ************************* Function *********************** //


		//参数实现
		public string ProjectName
		{
			get => _projectName;
			set
			{
				if(this._projectName != value)
				{
					this._projectName = value;
					ValidaProjectPath();
					OnPropertyChanged(nameof(ProjectName));
					
				}
			}
		}


		public string ProjectPath
		{
			get => _projectPath;
			set
			{
				if (this._projectPath != value)
				{
					this._projectPath = value;
					OnPropertyChanged(nameof(ProjectPath));

				}
			}
		}


		public bool IsValid
		{
			get => _isValid;
			set
			{
				if (this._isValid != value)
				{
					this._isValid = value;
					
					OnPropertyChanged(nameof(IsValid));

				}
			}
		}


		public int Count
		{
			get => g_count;
			set { g_count = value;
				OnPropertyChanged(nameof(Count));
			}
		}


		public string ErrorMsg
		{
			get => _errorMsg;
			set
			{
				if (this._errorMsg != value)
				{
					this._errorMsg = value;
					OnPropertyChanged(nameof(ErrorMsg));

				}
			}
		}


		public ReadOnlyObservableCollection<ProjectTemple> ProjectTemplates
		{
			get;
		}


		// 函数
		private bool ValidaProjectPath()
		{
			var path = ProjectPath;
			if(!Path.EndsInDirectorySeparator(path))
			{
				path += @"\";
			}
			path += $@"{ProjectName}\";

			IsValid = false;
			if (string.IsNullOrWhiteSpace(ProjectName.Trim()))
			{
				ErrorMsg = "Type in a project name";
			}
			else if (ProjectName.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
			{
				ErrorMsg = "Invalid character used in project name";
			}
			else if (string.IsNullOrWhiteSpace(ProjectPath.Trim()))
			{
				ErrorMsg = "Type in a project name";
			}
			else if (ProjectPath.IndexOfAny(Path.GetInvalidPathChars()) > -1)
			{
				ErrorMsg = "Invalid character used in project path";
			}
			else if ( Directory.Exists(path) && Directory.EnumerateFileSystemEntries(path).Any())
			{
				ErrorMsg = "Selected Project Floder is exists and is not empty";
			}else
			{
				ErrorMsg = String.Empty;
				IsValid = true;
			}

			return IsValid;


		}   //   验证项目路径


		public string CreateProject(ProjectTemple template)
		{
			ValidaProjectPath();

			if(!IsValid)
			{
				return string.Empty;
			}

			if (!Path.EndsInDirectorySeparator(ProjectPath)) ProjectPath += @"\";
			var path = $@"{ProjectPath}{ProjectName}\";

			try
			{
				if(!Directory.Exists(path)) Directory.CreateDirectory(path);
				foreach(var floder in template.Floders)
				{
					Directory.CreateDirectory(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(path), floder)));
				}

				var dirInfo = new DirectoryInfo(path + @".Primal\");
				dirInfo.Attributes |= FileAttributes.Hidden;
				File.Copy(template.IconFilePath, Path.GetFullPath(Path.Combine(dirInfo.FullName, "Icon.png")));
				File.Copy(template.ScreenShotFilePath, Path.GetFullPath(Path.Combine(dirInfo.FullName, "ScreenShot.png")));
				var projectXml = File.ReadAllText(template.ProjectFilePath);
				projectXml = String.Format(projectXml, ProjectName, ProjectPath);
				var projectPath = Path.GetFullPath(Path.Combine(path, $"{ProjectName}{Project.Extension}"));
				File.WriteAllText(projectPath, projectXml);

				return path;

			}
			catch (Exception ex)
			{
				Debug.Write(ex.Message);
				return string.Empty;
			}

		}


	}



}
