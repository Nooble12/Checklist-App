using CheckListApp.Classes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Xml.Serialization;

namespace CheckListApp.DataSaving
{
    public class SaveData
    {
        private string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private string saveFolderPath;
        public SaveData(ObservableCollection<TaskItem> inCollection)
        {
            saveFolderPath = Path.Combine(projectDirectory, "SaveFiles");
            CheckIfDirectoryExists(saveFolderPath);
        }

        private void CheckIfDirectoryExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Debug.WriteLine("Folder not found. Created new folder");
            }
        }
        public void Save(ObservableCollection<TaskItem> inCollection, string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<TaskItem>));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fileStream, inCollection);
            }
        }
        public void CheckIfDirectoryExists()
        {

            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
                Debug.WriteLine("Folder not found. Created new folder");
            }
        }
        public string GetSaveFolderDirectory()
        {
            return saveFolderPath;
        }
    }
}
