using MobileTaskEditor.Model;

namespace MobileTaskEditor.Pages
{
    public partial class TaskPage
    {
        public TaskPage()
        {
            InitializeComponent();
        }

        public TaskPage(TaskInfo argsItem) : this()
        {
            TaskInfoViewModel.TaskInfo = argsItem; // TODO: Is this the best way to pass in a model?
        }
    }
}