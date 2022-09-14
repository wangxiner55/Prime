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
	/// LoadProject.xaml 的交互逻辑
	/// </summary>
	public partial class LoadProject : UserControl
	{

		public LoadProject()
		{
			InitializeComponent();

			Loaded += (s, e) =>
			{
				var item = projectListBox.ItemContainerGenerator.ContainerFromIndex(projectListBox.SelectedIndex) as ListBoxItem;
				item?.Focus();
			};
		}


		private void On_OpenButton_Click(object sender, RoutedEventArgs e)
		{
			OpenClickedProject();
		}


		private void On_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			OpenClickedProject();
		}


		private void OpenClickedProject()
		{

			var project = OpenProject.Open(projectListBox.SelectedItem as ProjectData);
			bool dialogResult = false;
			var win = Window.GetWindow(this);
			if (project != null)
			{
				dialogResult = true;
				win.DataContext = project;
			}
			win.DialogResult = dialogResult;
			win.Close();
		}

		
	}
}
