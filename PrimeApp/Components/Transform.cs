using PrimeApp.Comman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Components
{
	public class Transform : Component
	{

		// ----------------------- Instance -------------------------- //



		private Vector3 _position;
		private Vector3 _rotation;
		private Vector3 _scale;



		// ----------------------- Instance -------------------------- //



		public Transform(GameEntity owner) : base(owner)
		{


		}



		// ----------------------- Function -------------------------- //



		[DataMember]
		public Vector3 Position
		{
			get { return _position; }
			set
			{
				if (_position != value)
				{
					_position = value;
					OnPropertyChanged(nameof(Position));
				}
			}
		}


		[DataMember]
		public Vector3 Rotation
		{
			get { return _rotation; }
			set
			{
				if (_rotation != value)
				{
					_rotation = value;
					OnPropertyChanged(nameof(Rotation));
				}
			}
		}


		[DataMember]
		public Vector3 Scale
		{
			get { return _scale; }
			set
			{
				if (_scale != value)
				{
					_scale = value;
					OnPropertyChanged(nameof(Scale));
				}
			}
		}



	}
}
