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

        public MainModel()
        {
            SaveTaskCommand = new SaveTaskCommand(this);
            NewTaskCommand = new RelayCommand(p => CurrentTask = new TaskInfo(), p => true);
        }

        public TaskInfo CurrentTask
        {
            get => _currentTask;
            set
            {
                if (Equals(value, _currentTask)) return;
                _currentTask = value;
                OnPropertyChanged();
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

        /// <summary>
        /// The conditions for making a new task is that there is no current task.
        /// </summary>
        public bool CanExecute(object parameter) => _model.CurrentTask != null;

        public void Execute(object parameter) => _model.CurrentTask.Saved = true;

        public event EventHandler CanExecuteChanged;

        public virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public class TaskInfo : INotifyPropertyChanged
    {
        private bool _saved;
        private string _description;

        public bool Saved
        {
            get => _saved;
            set
            {
                if (value == _saved) return;
                _saved = value;
                OnPropertyChanged();
            }
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
            if (propertyName != nameof(Saved)) Saved = false;
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}