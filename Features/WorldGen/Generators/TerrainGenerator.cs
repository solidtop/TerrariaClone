using System.Threading.Tasks;
using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public partial class TerrainGenerator : WorldGeneratorBase
    {
        private const int ChunkSize = 16;

        public override GenerationPass Pass => GenerationPass.Terrain;

        public override async Task Generate(TileType[,] tiles, WorldGenContext context)
        {
            var noiseConfig = context.Config.Terrain.HeightNoise;
            var splineConfig = context.Config.Terrain.HeightSpline;

            var octaves = noiseConfig.Octaves;
            var frequency = noiseConfig.Frequency;
            var amplitude = noiseConfig.Amplitude;
            var noise = new PerlinNoise(context.Seed, octaves, frequency, amplitude);

            var spline = new CubicSpline(splineConfig.ControlPoints);

            var worldSize = context.Definitions.World.Size;
            var seaLevel = context.Definitions.World.SeaLevel;

            for (int x = 0; x < worldSize.X; x++)
            {
                var height = spline.Interpolate(noise.Sample1D(x));

                for (int y = 0; y < worldSize.Y; y++)
                {
                    if (y >= height)
                    {
                        tiles[x, y] = TileType.Stone;
                    }
                    else if (y >= seaLevel)
                    {
                        tiles[x, y] = TileType.Dirt;
                    }
                    else
                    {
                        tiles[x, y] = TileType.Air;
                    }
                }

                await Yield(x, ChunkSize);

                var progress = (float)(x + 1) / worldSize.X;
                UpdateProgress(progress);
            }
        }
    }
}
