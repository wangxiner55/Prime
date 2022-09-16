﻿using PrimeApp.Appcation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
	/// <summary>
	/// WorldEditorView.xaml 的交互逻辑
	/// </summary>
	public partial class WorldEditorView : UserControl
	{

		public WorldEditorView()
		{

			InitializeComponent();

			Loaded += WorldEditorView_Loaded;

		}


		private void WorldEditorView_Loaded(object sender, RoutedEventArgs e)
		{

			Loaded -= WorldEditorView_Loaded;

			Focus();
			((INotifyCollectionChanged)Project.UndoRedo.UndoList).CollectionChanged += (s, e) => Focus();

		}

	}

}
