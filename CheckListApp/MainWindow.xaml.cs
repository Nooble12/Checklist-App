using CheckListApp.Classes;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CheckListApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public ObservableCollection<TaskItem> TaskList { get; set; } = new ObservableCollection<TaskItem>();
    public MainWindow()
    {
        InitializeComponent();
        TaskListBox.ItemsSource = TaskList;
    }

    private void AddTask_Click(object sender, RoutedEventArgs e)
    {
        TaskItem task = new TaskItem("N/A", 0);
        CreateTaskDialogBox(task);
        TaskList.Add(task);
    }

    private void SaveFile_Click(object sender, RoutedEventArgs e)
    {
        CreateSaveFileDialogBox();
    }

    private void LoadFile_Click(object sender, RoutedEventArgs e)
    {
        CreateLoadFileDialogBox();
    }
    private void ClearAllTask_Click(object sender, RoutedEventArgs e)
    {
        ClearAllTasks();
    }

    private void CreateTaskDialogBox(TaskItem inTask)
    {
        var inputWindow = new TaskCreationWindow(inTask);
        bool? result = inputWindow.ShowDialog();
    }

    private void CreateSaveFileDialogBox()
    {
        var saveFileWindow = new SaveFileWindow(TaskList);
        bool? result = saveFileWindow.ShowDialog();
    }

    private void ClearAllTasks()
    {
        TaskList.Clear();
    }

    private void CreateLoadFileDialogBox()
    {
        var loadFileWindow = new LoadFileWindow();
        bool? result = loadFileWindow.ShowDialog();
        ObservableCollection<TaskItem> tempCollection = loadFileWindow.GetCollectionData();

        if (tempCollection == null)
        {
            return;
        }

        ClearAllTasks();
        foreach (var task in tempCollection)
        {
            TaskList.Add(task);
        }
    }

    private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            var instance = button.DataContext;
            switch (instance)
            {
                case TaskItem item:
                    TaskList.Remove(item);
                break;
            }
        }
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button addButton)
        {
            var instance = addButton.DataContext;
            switch (instance)
            {
                case TaskItem item:
                    item.CurrentGoalValue += 1;
                break;
            }
        }
    }

    private void SubtractButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button subtractButton)
        {
            var instance = subtractButton.DataContext;
            switch (instance)
            {
                case TaskItem item:
                    if (item.CurrentGoalValue > 0)
                    {
                        item.CurrentGoalValue--;
                    }
                    break;
            }
        }
    }
}