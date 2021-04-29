using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MobileTaskEditor.Annotations;

namespace MobileTaskEditor
{
    public sealed class MainModel : INotifyPropertyChanged
    {
        private TaskInfo _currentTask;
        private SaveTaskCommand _saveTaskCommand;
        private RelayCommand _newTaskCommand;
        private bool _currentTaskSaved;

        public bool CurrentTaskSaved
        {
            get => _currentTaskSaved;
            set
            {
                if (value == _currentTaskSaved) return;
                _currentTaskSaved = value;
                OnPropertyChanged();
                SaveTaskCommand.OnCanExecuteChanged();
            }
        }

        public MainModel()
        {
            SaveTaskCommand = new SaveTaskCommand(this);
            NewTaskCommand = new RelayCommand(p =>
            {
                CurrentTask = new TaskInfo(this);
                CurrentTaskSaved = false;
            }, p => true);
        }

        public TaskInfo CurrentTask
        {
            get => _currentTask;
            set
            {
                if (Equals(value, _currentTask)) return;
                _currentTask = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTask));
                SaveTaskCommand.OnCanExecuteChanged();
            }
        }

        public SaveTaskCommand SaveTaskCommand
        {
            get => _saveTaskCommand;
            set
            {
                if (Equals(value, _saveTaskCommand)) return;
                _saveTaskCommand = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NewTaskCommand
        {
            get => _newTaskCommand;
            set
            {
                if (Equals(value, _newTaskCommand)) return;
                _newTaskCommand = value;
                OnPropertyChanged();
            }
        }

        public bool HasTask => CurrentTask != null;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class SaveTaskCommand : ICommand
    {
        private readonly MainModel _model;

        public SaveTaskCommand(MainModel model)
        {
            _model = model;
        }

        public bool CanExecute(object parameter) => _model.CurrentTask != null && !_model.CurrentTaskSaved;

        public void Execute(object parameter)
        {
            _model.CurrentTaskSaved = true;
            OnCanExecuteChanged();
        }

        public event EventHandler CanExecuteChanged;

        public virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class TaskInfo : INotifyPropertyChanged
    {
        private readonly MainModel _model;
        private string _description;

        public TaskInfo(MainModel model)
        {
            _model = model;
        }

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _model.CurrentTaskSaved = false;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}