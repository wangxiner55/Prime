using PrimeApp.Appcation;
using PrimeApp.Comman;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Components
{

	[DataContract]
	public class GameEntity : ViewModeBase
	{

		// ----------------------- Member -------------------------- //



		private string _name;

		private readonly ObservableCollection<Component> _components = new ObservableCollection<Component>();



		// ----------------------- Instance -------------------------- //



		public GameEntity(Scene scene)
		{
			Debug.Assert(scene != null);
			ParentScene = scene;
		}



		// ----------------------- Function -------------------------- //



		[DataMember]
		public string Name
		{
			get { return _name; }
			set
			{
				if(_name != value)
				{
					_name = value;
					OnPropertyChanged();
				}
			}
		}


		[DataMember]
		public Scene ParentScene { get; private set; }


		[DataMember(Name = nameof(Compoents))]
		public ReadOnlyObservableCollection<Component> Compoents { get; }



	}

}
