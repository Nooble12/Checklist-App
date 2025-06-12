using CheckListApp.Classes;
using System.Windows;
using System.Windows.Controls;

namespace CheckListApp
{
    /// <summary>
    /// Interaction logic for TaskCreationWindow.xaml
    /// </summary>
    public partial class TaskCreationWindow : Window
    {
        private TaskItem task;
        private string newTaskName = "N/A";
        public TaskCreationWindow(TaskItem inTask)
        {
            InitializeComponent();
            task = inTask;
        }

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            newTaskName = InputTextBox.Text;
        }

        private void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            task.TaskName = newTaskName;
            this.Close();
        }
    }
}
