using System;
using System.Windows.Input;

namespace MobileTaskEditor
{
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
}