﻿using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrimeApp.Appcation
{
	/// <summary>
	/// NewProject.xaml 的交互逻辑
	/// </summary>
	public partial class NewProject : UserControl
	{

		public NewProject()
		{
			InitializeComponent();

		}


		private void On_Create_Click(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as NewProjects;
			var projectPath = vm.CreateProject(ProjectTmeplateListBox.SelectedItem as ProjectTemple);
			bool dialogResult = false;
			var win = Window.GetWindow(this);
			if (!string.IsNullOrEmpty(projectPath))
			{
				dialogResult = true;
				var project = OpenProject.Open(new ProjectData() { ProjectName = vm.ProjectName, ProjectPath = projectPath });
				win.DataContext = project;
			}
			win.DialogResult = dialogResult;
			win.Close();
		}
	}
}
