using System;
using System.Windows.Input;
using MobileTaskEditor.ViewModel;

namespace MobileTaskEditor.Commands
{
    public sealed class SaveTaskCommand : ICommand
    {
        private readonly TaskInfoViewModel _model;

        public SaveTaskCommand(TaskInfoViewModel model)
        {
            _model = model;
        }

        public bool CanExecute(object parameter) => _model.TaskInfo != null && _model.TaskInfo.IsChanged;

        public void Execute(object parameter)
        {
            // TODO: Save the task somewhere
            _model.TaskInfo.AcceptChanges();
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}