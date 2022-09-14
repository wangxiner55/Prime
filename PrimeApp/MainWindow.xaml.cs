using PrimeApp.Appcation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PrimeApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{

			InitializeComponent();

			Loaded += OnMainWindowLoaded;
			Closing += OnMainWindowClosing;
		}


		private void OnMainWindowClosing(object? sender, CancelEventArgs e)
		{

			Closing -= OnMainWindowClosing;
			//Project.Current?.unLoad();
		}


		private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
		{

			Loaded -= OnMainWindowLoaded;

			OpenProjectDialog();
			
		}


		private void OpenProjectDialog()
		{
			
			var projectBrower = new APPLoginDialog();
			if (projectBrower.ShowDialog() == false)
			{
				Application.Current.Shutdown();
			}
			else
			{
				Project.Current?.unLoad();
				DataContext = projectBrower.DataContext;
				
			}
		}


		private void WorldEditorView_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}
