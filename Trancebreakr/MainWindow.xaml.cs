using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32.TaskScheduler;
using Xceed;

namespace Trancebreakr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string mainProgramKey = "Trancebreakr";
        public MainWindow()
        {
            InitializeComponent();
            UpdateScheduledTasksList();
        }

        private void SetScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            var timeOfDay = timeOfDayTextBox.Text;
            var numberOfMinutesWarning = numberOfMinutesWarningTextBox.Text;
            if(timeOfDay == null || timeOfDay == "Textbox")
            {
                MessageBox.Show("Enter a time to set the alarm. ");
                return;
            }
            DateTime timeOfDayDateTime = DateTime.Parse(timeOfDay);

            var numberOfMinutesWarningInt = 10;
            int.TryParse(numberOfMinutesWarning, out numberOfMinutesWarningInt);
            if (numberOfMinutesWarningInt.GetType() != typeof(int))
            {
                MessageBox.Show("Enter a number of minutes for warning needed.");
                return;
            }
            DateTime warningDateTime = timeOfDayDateTime.Add(new TimeSpan(0, -numberOfMinutesWarningInt, 0));

            //TODO: set the alarms
            
            //CreateScheduledTask("MyTestTask", "cmd", "");
            CreateScheduledTask($"{mainProgramKey}ShutdownWarning", "cmd", $"/C TITLE Shutdown coming soon!&ECHO.& ECHO.& ECHO Computer shutting down in {numberOfMinutesWarningInt} minutes!&ECHO.& ECHO.& TIMEOUT 3", warningDateTime, $"Warning {numberOfMinutesWarningInt}m before shutdown");
            CreateScheduledTask($"{mainProgramKey}Shutdown", "cmd", $"/C Rundll32.exe Powrprof.dll,SetSuspendState Hibernate", timeOfDayDateTime, "Shuts down computer");
            UpdateScheduledTasksList();
            MessageBox.Show($"Time of day to set alarm: {timeOfDay}, Number of minutes beforehand to give a warning: {numberOfMinutesWarningInt}");

        }

        public void CreateScheduledTask(string taskName, string taskProgramTrigger, string taskCommand, DateTime startTime, string taskDescription)
        {
            // Get the service on the local machine
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = taskDescription ?? "Does something";

                // Logged on or not with highest privileges 
                td.Principal.LogonType = TaskLogonType.S4U;
                td.Principal.RunLevel = TaskRunLevel.Highest;

                // Create a trigger that will fire the task at this time every other day
                td.Triggers.Add(new DailyTrigger { DaysInterval = 1, StartBoundary = startTime });

                // Create an action that will launch Notepad whenever the trigger fires
                td.Actions.Add(new ExecAction(taskProgramTrigger, taskCommand, null));

                // Register the task in the root folder
                ts.RootFolder.RegisterTaskDefinition(@taskName, td);

                // Remove the task we just created
                //ts.RootFolder.DeleteTask("Test");
            }
        }

        private List<string> GetOurScheduledTasks(string programKey)
        {
            string lowercaseProgramKey = programKey.ToLower();
            List<string> programTasksList = new List<string>();
            using (TaskService ts = new TaskService())
            {
                foreach (Microsoft.Win32.TaskScheduler.Task task in ts.RootFolder.Tasks)
                {
                    if (task.Name.ToLower().Contains(lowercaseProgramKey))
                    {
                        programTasksList.Add(task.Name + " - " + string.Format("{0:hh:mm:ss tt}", task.NextRunTime));
                    }
                }
            }
            return programTasksList;
        }

        public void UpdateScheduledTasksList()
        {
            List<string> programTasksList = GetOurScheduledTasks(mainProgramKey);
            existingScheduledTasks.Items.Clear();
            if (programTasksList.Count > 0)
            {
                foreach (string s in programTasksList)
                {

                    ListBoxItem itm = new ListBoxItem();
                    itm.Content = s;
                    existingScheduledTasks.Items.Add(itm);
                }
            } else
            {
                ListBoxItem itm = new ListBoxItem();
                itm.Content = "No tasks scheduled";
                existingScheduledTasks.Items.Add(itm);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (TaskService ts = new TaskService())
            {
                bool task1Found = false;
                foreach (Microsoft.Win32.TaskScheduler.Task task in ts.RootFolder.Tasks)
                {
                    if(task.Name.Contains(mainProgramKey))
                    {
                        ts.RootFolder.DeleteTask(task.Name);
                        task1Found = true;
                    } 
                }
                if(task1Found)
                {
                    MessageBox.Show("Scheduled tasks deleted.");
                } else
                {
                    MessageBox.Show("Scheduled tasks weren't found so they weren't deleted.");
                }
                UpdateScheduledTasksList();

            }
        }

        private void refreshTasks_Click(object sender, RoutedEventArgs e)
        {
            UpdateScheduledTasksList();
        }
    }
}
