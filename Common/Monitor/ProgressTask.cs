using System.Collections.Generic;

namespace TerrariaClone.Common.Monitor
{
    public class ProgressTask(string name, float totalWork, ProgressTask parent = null)
    {
        public string Name { get; } = name;
        public float TotalWork { get; } = totalWork;
        public ProgressTask Parent { get; } = parent;
        public List<ProgressTask> Children { get; } = [];
        public float WorkDone { get; private set; } = 0;

        public void ReportProgress(int work)
        {
            WorkDone += work;

            if (WorkDone > TotalWork)
                WorkDone = TotalWork;
        }

        public float GetProgress()
        {
            if (Children.Count == 0)
            {
                return WorkDone / TotalWork;
            }

            var total = 0f;
            var weightedSum = 0f;

            foreach (var child in Children)
            {
                total += child.TotalWork;
                weightedSum += child.GetProgress() * child.TotalWork;
            }

            return weightedSum / total;
        }

        public void Complete() => WorkDone = TotalWork;
    }
}