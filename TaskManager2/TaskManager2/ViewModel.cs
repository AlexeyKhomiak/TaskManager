using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace TaskManager2
{
    public class ViewModel
    {
        public ObservableCollection<Process> Processes { get; set; }
        public Process CurrentProcess { get; set; }
        public List<int> currentIds;

        public ViewModel()
        {
            Processes = new ObservableCollection<Process>();
            
            currentIds = Processes.Select(x => x.Id).ToList();

            foreach (var i in Process.GetProcesses())
            {
                Processes.Add(i);
            }

            StopProcess = new RelayCommand(x =>
            {
                CurrentProcess.CloseMainWindow();
                CurrentProcess.Kill();
                Processes.Remove(CurrentProcess);
                foreach (var i in Process.GetProcesses())
                    {
                        Processes.Add(i);
                    }
            });

            RefreshProcess = new RelayCommand(x =>
            {
                Processes.Clear();
                foreach (var i in Process.GetProcesses())
                {
                    Processes.Add(i);
                }
            });

        }

        public ICommand StopProcess { get; set; }
        public ICommand RefreshProcess { get; set; }


    }
}
