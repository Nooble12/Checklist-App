using CheckListApp.Classes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Xml.Serialization;

namespace CheckListApp.DataSaving
{
    public class LoadData
    {
        private string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private string saveFolderPath;

        public LoadData()
        {
            saveFolderPath = Path.Combine(projectDirectory, "SaveFiles");
        }

        public ObservableCollection<TaskItem> GetFileData(string inFilePath)
        {
            ObservableCollection<TaskItem> collection;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<TaskItem>));
            using (FileStream fileStream = new FileStream(inFilePath, FileMode.Open))
            {
                collection = (ObservableCollection<TaskItem>)xmlSerializer.Deserialize(fileStream);
            }
            return collection;
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
