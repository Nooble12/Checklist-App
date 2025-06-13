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
        private ushort goalValue = 0;
        private bool goalValueIsValid = false;
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
            if (goalValueIsValid)
            {
                task.TaskName = newTaskName;
                task.FinalGoalValue = goalValue;
                task.IsGoalActive = true;
                Close();
            }
            else
            {
                DisplayErrorMessage("Error, invalid inputs");
            }
        }

        private void FinalGoalTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ushort number = 0;
            bool success = ushort.TryParse(finalGoalTextBox.Text, out number);

            if (success && number < ushort.MaxValue)
            {
                goalValue = number;
                goalValueIsValid = true;

                if (ErrorLabel != null)
                {
                    ErrorLabel.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                DisplayErrorMessage("Error, invalid ushort");
                goalValueIsValid = false;
            }
        }

        private void DisplayErrorMessage(string inMessage)
        {
            ErrorLabel.Visibility = Visibility.Visible;
            ErrorLabel.Content = inMessage;
        }

        private void GoalCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (GoalCheckBox.IsChecked == true)
            {
                GoalTabPanel.Visibility = Visibility.Visible;
            }
            else
            {
                GoalTabPanel.Visibility = Visibility.Hidden;
            }
        }
    }
}
