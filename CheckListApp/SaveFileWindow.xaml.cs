using CheckListApp.Classes;
using CheckListApp.DataSaving;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CheckListApp
{
    /// <summary>
    /// Interaction logic for SaveFileWindow.xaml
    /// </summary>
    public partial class SaveFileWindow : Window
    {
        private ObservableCollection<TaskItem> collection;
        private SaveData data;
        private string newFileName = "N/A";
        public SaveFileWindow(ObservableCollection<TaskItem> inCollection)
        {
            InitializeComponent();
            collection = inCollection;
            data = new SaveData(inCollection);
            data.CheckIfDirectoryExists();
            DisplaySaveFiles();
        }

        private void DisplaySaveFiles()
        {
            string[] files = Directory.GetFiles(data.GetSaveFolderDirectory());

            foreach (string file in files)
            {
                FileListBox.Items.Add(Path.GetFileName(file));
            }
        }

        private void FileNameTextBox_Changed(object sender, EventArgs e)
        {
            newFileName = FileNameTextBox.Text;
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "N/A";
            if (FileListBox.SelectedItem == null)
            {
                Debug.WriteLine("New File Name: " + newFileName);
                filePath = Path.Combine(data.GetSaveFolderDirectory(), newFileName + ".xml");

                data.Save(collection, filePath);
                Close();
                return;
            }
            else
            {
                string selectedFile = FileListBox.SelectedItem.ToString();
                filePath = Path.Combine(data.GetSaveFolderDirectory(), selectedFile);
            }

            try
            {
                string fileContent = File.ReadAllText(filePath);
                MessageBox.Show($"File Saved: {filePath}\nContent:\n{fileContent}");
                data.Save(collection, filePath);
            }
            catch (Exception ex)
                {
                    MessageBox.Show($"Failed to Save file: {ex.Message}");
                }
            Close();
        }

        private void OpenDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Open the SaveFiles directory in the default file explorer
                Close();
                System.Diagnostics.Process.Start("explorer.exe", data.GetSaveFolderDirectory());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open directory: {ex.Message}");
            }
        }
        private void searchBox_Changed(object sender, EventArgs e)
        {
            //Ignores the initial text changed on window start
            if (SearchBox.Text.Equals("Search Here"))
            {
                return;
            }
            DisplaySearchedFile(SearchBox.Text);
        }
        private void DisplaySearchedFile(string targetSearch)
        {
            string[] fileArray = Directory.GetFiles(data.GetSaveFolderDirectory());

            FileListBox.Items.Clear();

            foreach (string file in fileArray)
            {
                if (Path.GetFileName(file).IndexOf(targetSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    FileListBox.Items.Add(Path.GetFileName(file));
                }
            }
        }
    }

}
