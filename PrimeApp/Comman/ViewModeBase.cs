using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Comman
{

	[DataContract(IsReference =true)]
	public class ViewModeBase : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler? PropertyChanged;


		protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
