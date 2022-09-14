using PrimeApp.Appcation;
using PrimeApp.Components;
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

namespace PrimeApp.Editors
{

	public partial class ProjectLayoutControl : UserControl
	{

		public ProjectLayoutControl()
		{
			InitializeComponent();
		}


		private void OnAddGameEntityBotton_Click(object sender , RoutedEventArgs e)
		{
			var btn = sender as Button;
			var vm = btn.DataContext as Scene;
			vm.AddGameEntityCommand.Execute(new GameEntity(vm) { Name = "Empty Entity" });
		}


		private void OnRemoveGameEntityBotton_Click(object sender, RoutedEventArgs e)
		{
			var btn = sender as Button;
			var vm = btn.DataContext as Scene;
			vm.RemoveGameEntityCommand.Execute(new GameEntity(vm) { Name = "Empty Entity" });
		}

	}

}
