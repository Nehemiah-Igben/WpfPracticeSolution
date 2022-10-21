using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfPractice.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            //SumCommand = new SumCommand();
            //SumCommand = new DelegateCommand(() => { Result = NumOne + NumTwo; }, SumCommandCanExecute);
            SumCommand = new DelegateCommand<double>(OnSum, CanSumAction);
        }

        private bool CanSumAction(double arg)
        {
            return true;
        }

        private void OnSum(double obj)
        {
            Result = NumOne + NumTwo;
        }

        private bool SumCommandCanExecute()
        {
            return NumOne > 0 && NumTwo > 0;
        }

        //private void OnSumAction()
        //{
        //    Result = NumOne + NumTwo;
        //}

        private double numOne;
        public double NumOne
        {
            get => numOne;
            set 
            {
                numOne = value;
                OnPropertyChanged();
                SumCommand.RaiseCanExecuteChanged();
            }
        }

        private double numTwo;
        public double NumTwo
        {
            get => numTwo;
            set 
            { 
                numTwo = value;
                OnPropertyChanged(nameof(NumTwo));
                SumCommand.RaiseCanExecuteChanged();
            }
        }
        private double result;

        public double Result
        {
            get => result;
            set 
            { 
                result = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public SumCommand SumCommand { get; }


        //public DelegateCommand SumCommand { get; }
        public DelegateCommand<double> SumCommand { get; }
    }

    public class DelegateCommand<T> : ICommand
    {
        private Action<T> executeCallback;
        private Func<T, bool> canExecuteCallback;

        public DelegateCommand(Action<T> action, Func<T, bool> canExecuteAction = null)
        {
            executeCallback = action;
            canExecuteCallback = canExecuteAction;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter == null) return false;

            var param = (T)parameter;

            if (canExecuteCallback == null) return true;
            return canExecuteCallback(param);
        }

        public void Execute(object parameter)
        {
            var param = (T)parameter;
            if (param == null) return;

            executeCallback(param);
        }
    }

    public class DelegateCommand : ICommand
    {
        private Action executeCallback;
        private Func<bool> canExecuteCallback;

        public DelegateCommand(Action action, Func<bool> canExecuteAction = null)
        {
            executeCallback = action;
            canExecuteCallback = canExecuteAction;
        } 

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (canExecuteCallback == null) return true;
            return canExecuteCallback();
        }

        public void Execute(object parameter) => this.executeCallback();
    }

    public class SumCommand : ICommand
    {

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var viewModel = (MainWindowViewModel)parameter;
            if (viewModel == null) return false;

            return viewModel.NumOne > 0 && viewModel.NumTwo > 0;
        }

        public void Execute(object parameter)
        {
            var viewModel = (MainWindowViewModel)parameter;
            if (viewModel == null) return;

            viewModel.Result = viewModel.NumOne + viewModel.NumTwo;
        }
    }
}
