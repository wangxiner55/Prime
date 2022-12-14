using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrimeApp
{
	class RelayCommand<T> : ICommand
	{

		// --------------------- Member -------------------- //



		private readonly Action<T> _execute;

		private readonly Predicate<T> _canExecute;



		// --------------------- Instance -------------------- //



		public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
		{

			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;

		}



		// --------------------- Function -------------------- //



		public event EventHandler? CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}


		public bool CanExecute(object? parameter)
		{
			return _canExecute?.Invoke((T)parameter) ?? true;
		}


		public void Execute(object? parameter)
		{
			_execute?.Invoke((T)parameter);
		}


	}


}
