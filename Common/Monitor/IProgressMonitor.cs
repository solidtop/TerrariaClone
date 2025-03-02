using System;

namespace TerrariaClone.Common.Monitor
{
    public interface IProgressMonitor
    {
        event Action<string, float> ProgressChanged;

        void BeginTask(string taskName, int subTasks, float totalWork = 100);
        void BeginSubTask(string taskName, float totalWork = 100);
        void ReportProgress(int workDone);
        void CompleteTask();
        void Cancel();
        float CurrentProgress { get; }
        string CurrentTaskName { get; }
    }
}
