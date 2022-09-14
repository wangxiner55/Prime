using PrimeApp.Comman;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Components
{

	public class Component : ViewModeBase
	{

		public GameEntity Owner { get; set; }


		public Component (GameEntity owner)
		{
			Debug.Assert(owner != null);
			Owner = owner;
		}


	}

}
