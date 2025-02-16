using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Configurations;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public abstract class WorldGeneratorBase : IWorldGenerator
    {
        public abstract WorldGenPass Pass { get; }
        public abstract void Generate(TileType[,] tiles, TileRegion region, WorldGenContext context);

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
