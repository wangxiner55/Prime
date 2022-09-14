using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeApp.Ultites
{

	public interface IUndoRedo
	{
		string Name { get;}

		void Undo();
		void Redo();
	}




	public class UndoRedoAction : IUndoRedo
	{

		private Action _undoAction;
		private Action _redoAction;


		public string Name { get; }

		public void Redo() => _redoAction();
		public void Undo() => _undoAction();

		public UndoRedoAction(Action undo,Action redo,string name)
			
		{
			Debug.Assert(undo != null&&redo != null);
			_undoAction = undo;
			_redoAction = redo;
			Name = name;
		}
	}





	public class UndoRedo
	{

		private readonly ObservableCollection<IUndoRedo> _UndoList = new ObservableCollection<IUndoRedo>();
		private readonly ObservableCollection<IUndoRedo> _RedoList = new ObservableCollection<IUndoRedo>();

		public ReadOnlyObservableCollection<IUndoRedo> UndoList { get; }
		public ReadOnlyObservableCollection<IUndoRedo> RedoList { get; }


		public void Reset()
		{
			_UndoList.Clear();
			_RedoList.Clear();
		}


		public void Add(IUndoRedo cmd)
		{
			_UndoList.Add(cmd);
			_RedoList.Clear();
		}


		public void Undo()
		{
			if (_UndoList.Any())
			{
				var cmd = _UndoList.Last();
				_UndoList.RemoveAt(_UndoList.Count - 1);
				cmd.Undo();
				_RedoList.Insert(0, cmd);
			}
		}


		public void Redo()
		{
			if(_RedoList.Any())
			{
				var cmd = _RedoList.First();
				_RedoList.RemoveAt(0);
				cmd.Redo();
				_UndoList.Add(cmd);
			}
		}


		public UndoRedo()
		{
			UndoList = new ReadOnlyObservableCollection<IUndoRedo>(_UndoList);
			RedoList = new ReadOnlyObservableCollection<IUndoRedo>(_RedoList);
		}


	}




}
