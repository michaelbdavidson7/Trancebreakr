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
        public MainWindow()
        {
            InitializeComponent();
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
            CreateScheduledTask("TrancebreakrShutdownWarning", "cmd", $"/C TITLE Shutdown coming soon!&ECHO.& ECHO.& ECHO Computer shutting down in {numberOfMinutesWarningInt} minutes!&ECHO.& ECHO.& TIMEOUT 3", timeOfDayDateTime);
            CreateScheduledTask("TrancebreakrShutdown", "cmd", $"/C Rundll32.exe Powrprof.dll,SetSuspendState Hibernate", warningDateTime);
            MessageBox.Show($"Time of day to set alarm: {timeOfDay}, Number of minutes beforehand to give a warning: {numberOfMinutesWarningInt}");
        }

        public void CreateScheduledTask(string taskName, string taskProgramTrigger, string taskCommand, DateTime startTime)
        {
            // Get the service on the local machine
            using (TaskService ts = new TaskService())
            {
                // Create a new task definition and assign properties
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Does something";

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (TaskService ts = new TaskService())
            {
                string taskName1 = "TrancebreakrShutdownWarning";
                string taskName2 = "TrancebreakrShutdown";
                bool task1Found = false;
                bool task2found = false;
                foreach (Microsoft.Win32.TaskScheduler.Task task in ts.RootFolder.Tasks)
                {
                    if(task.Name == taskName1)
                    {
                        task1Found = true;
                    } else if (task.Name == taskName2)
                    {
                        task2found = true;
                    }
                }
                if(task1Found && task2found)
                {
                    ts.RootFolder.DeleteTask(taskName1);
                    ts.RootFolder.DeleteTask(taskName2);
                    MessageBox.Show("Scheduled tasks deleted.");
                } else
                {
                    MessageBox.Show("Scheduled tasks weren't found so they weren't deleted.");
                }


            }
        }
    }
}
