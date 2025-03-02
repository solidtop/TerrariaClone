using Godot;
using TerrariaClone.Common.Monitor;

namespace TerrariaClone.Features.WorldGen.Debug
{
    public partial class WorldGenProgressReporter(IProgressMonitor progressMonitor)
    {
        private readonly IProgressMonitor _progressMonitor = progressMonitor;

        private void OnProgressUpdated(string taskName, float progress)
        {
            GD.Print($"{taskName}: {progress * 100}%");
        }
    }
}
