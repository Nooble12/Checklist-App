using System.Xml.Serialization;

namespace CheckListApp.Classes
{
    public class TaskItem
    {
        [XmlElement("TaskName")]
        public string TaskName {get; set;}

        [XmlElement("TaskIsCompleted")]
        public bool IsCompleted { get; set; } = false;

        public TaskItem(string InTaskName)
        {
            TaskName = InTaskName;
        }

        public TaskItem()
        {

        }
    }
}
