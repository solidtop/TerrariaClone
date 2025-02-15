using System;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public abstract class WorldGeneratorBase : IWorldGenerator
    {
        private static readonly MainLoop MainLoop = Engine.GetMainLoop();

        public event Action<GenerationProgressInfo> ProgressUpdated;

        public abstract GenerationPass Pass { get; }
        public abstract Task Generate(TileType[,] tiles, WorldGenContext context);

        protected static async Task Yield(int index, int chunkSize)
        {
            if (index % chunkSize == 0)
            {
                await MainLoop.ToSignal(MainLoop, SceneTree.SignalName.ProcessFrame);
            }
        }

        protected void UpdateProgress(float progress)
        {
            ProgressUpdated?.Invoke(new GenerationProgressInfo(Pass, progress));
        }
    }
}
