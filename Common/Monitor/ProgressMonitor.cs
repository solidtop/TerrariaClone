using System;
using System.Collections.Generic;

namespace TerrariaClone.Common.Monitor
{
    public class ProgressMonitor : IProgressMonitor
    {
        private readonly Stack<ProgressTask> _activeTasks = [];

        public event Action<string, float> ProgressChanged;

        public void BeginTask(string taskName, int subTasks, float totalWork = 100)
        {
            var mainTask = new ProgressTask(taskName, totalWork);

            for (int i = 0; i < subTasks; i++)
            {
                mainTask.Children.Add(new ProgressTask("Pending", 100, mainTask));
            }

            _activeTasks.Clear();
            _activeTasks.Push(mainTask);
            NotifyProgressChanged();
        }

        public void BeginSubTask(string taskName, float totalWork = 100)
        {
            if (_activeTasks.Count == 0)
                throw new InvalidOperationException("No active task to attach a subtask to.");

            var parent = _activeTasks.Peek();
            var index = parent.Children.FindIndex(child => child.Name == "Pending");

            if (index == -1)
                throw new InvalidOperationException("No pending subtask available.");

            var subTask = new ProgressTask(taskName, totalWork, parent);
            parent.Children[index] = subTask;
            _activeTasks.Push(subTask);
            NotifyProgressChanged();
        }

        public void ReportProgress(int workDone)
        {
            ThrowIfEmpty();

            var current = _activeTasks.Peek();
            current.ReportProgress(workDone);
            NotifyProgressChanged();
        }

        public void CompleteTask()
        {
            ThrowIfEmpty();

            var current = _activeTasks.Pop();
            current.Complete();
            NotifyProgressChanged();
        }

        public void Cancel()
        {
            _activeTasks.Clear();
            NotifyProgressChanged();
        }

        public float CurrentProgress => _activeTasks.Peek().GetProgress();
        public string CurrentTaskName => _activeTasks.Peek().Name;

        private void NotifyProgressChanged()
        {
            if (_activeTasks.Count == 0)
                return;

            var current = _activeTasks.Peek();
            ProgressChanged?.Invoke(current.Name, current.GetProgress());
        }

        private void ThrowIfEmpty()
        {
            if (_activeTasks.Count == 0)
                throw new InvalidOperationException("No active task.");
        }
    }
}
