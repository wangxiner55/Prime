using PrimeApp.Appcation;
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
using System.Windows.Shapes;

namespace PrimeApp.Appcation
{
	/// <summary>
	/// APPLoginDialog.xaml 的交互逻辑
	/// </summary>
	public partial class APPLoginDialog : Window
	{
		public APPLoginDialog()
		{
			InitializeComponent();
		}

		private void OnToggleButton_Click(object sender, RoutedEventArgs e)
		{
			if(sender == LoadProject)
			{
				if(NewProject.IsChecked == true)
				{
					NewProject.IsChecked = false;
					browerContent.Margin = new Thickness(0);
				}
				LoadProject.IsChecked = true;

			}
			else
			{
				if (LoadProject.IsChecked == true)
				{
					LoadProject.IsChecked = false;
					browerContent.Margin = new Thickness(-840, 0, 0, 0);
				}
				NewProject.IsChecked = true;

			}
		}

	}
}
