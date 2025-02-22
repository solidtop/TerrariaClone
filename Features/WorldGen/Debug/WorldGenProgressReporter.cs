using Godot;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Debug
{
    public partial class WorldGenProgressReporter : IWorldGenDebugger
    {
        private WorldGenerator _worldGenerator;

        public void SetWorldGenerator(WorldGenerator worldGenerator)
        {
            _worldGenerator = worldGenerator;
            _worldGenerator.ProgressUpdated += OnProgressUpdated;
        }

        private void OnProgressUpdated(WorldGenPass pass, int percentage)
        {
            GD.Print($"{WorldGenPassInfo.GetDescription(pass)}: {percentage}%");
        }
    }
}
