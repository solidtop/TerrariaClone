using System;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Configurations;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public abstract class WorldGeneratorBase : IWorldGenerator
    {
        private static readonly MainLoop MainLoop = Engine.GetMainLoop();

        public event Action<WorldGenProgressInfo> ProgressUpdated;

        public abstract WorldGenPass Pass { get; }
        public abstract int ChunkSize { get; }
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
            ProgressUpdated?.Invoke(new WorldGenProgressInfo(Pass, progress));
        }

        protected static PerlinNoise CreateNoise(int seed, NoiseConfig config)
        {
            return new(seed, config.Octaves, config.Frequency, config.Amplitude);
        }

        protected static CubicSpline CreateSpline(SplineConfig config)
        {
            return new(config.ControlPoints);
        }
    }
}
