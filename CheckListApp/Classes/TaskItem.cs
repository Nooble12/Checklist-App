using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Media;
using System.Xml.Serialization;

namespace CheckListApp.Classes
{
    public class TaskItem : INotifyPropertyChanged
    {
        [XmlElement("TaskName")]
        public string TaskName {get; set;}

        [XmlElement("FinalGoalValue")]
        public ushort FinalGoalValue { get; set; } = 0;

        [XmlElement("IsGoalActive")]
        public bool IsGoalActive;

        public TaskItem(string InTaskName, ushort inFinalGoalValue)
        {
            TaskName = InTaskName;
            FinalGoalValue = inFinalGoalValue;
        }

        public TaskItem()
        {

        }

        private bool _isCompleted;
        [XmlElement("TaskIsCompleted")]
        public bool IsCompleted {
            get => _isCompleted;
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;

                    if (_isCompleted)
                    {
                        //Debug.WriteLine("Task: " + TaskName + " has been completed!");
                        ForegroundTextColor = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    }
                    else
                    {
                        ForegroundTextColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    }
                        OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        private ushort _currentGoalValue;
        [XmlElement("CurrentGoalValue")]
        public ushort CurrentGoalValue
        {
            get => _currentGoalValue;
            set
            {
                if (_currentGoalValue != value)
                {
                    _currentGoalValue = value;

                    if (_currentGoalValue >= FinalGoalValue)
                    {
                        IsCompleted = true;
                    }
                    else
                    {
                        IsCompleted = false;
                    }

                    OnPropertyChanged(nameof(CurrentGoalValue));
                }
            }
        }

        private Brush _foregroundTextColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        [XmlIgnore]
        public Brush ForegroundTextColor
        {
            get => _foregroundTextColor;
            set
            {
                if(_foregroundTextColor != value)
                {
                    //Debug.WriteLine("Color supposed to change?");
                    _foregroundTextColor = value;
                    OnPropertyChanged(nameof(ForegroundTextColor));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
