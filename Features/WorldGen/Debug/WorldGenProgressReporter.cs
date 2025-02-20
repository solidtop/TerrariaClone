using Godot;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Debug
{
    public partial class WorldGenProgressReporter(WorldGenerator worldGenerator) : Node
    {
        private readonly WorldGenerator _worldGenerator = worldGenerator;

        public override void _Ready()
        {
            _worldGenerator.ProgressUpdated += OnProgressUpdated;
        }

        private void OnProgressUpdated(WorldGenPass pass, int percentage)
        {
            GD.Print($"{WorldGenPassInfo.GetDescription(pass)}: {percentage}%");
        }
    }
}
