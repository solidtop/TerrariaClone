using System.Threading.Tasks;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public partial class TerrainGenerator : WorldGeneratorBase
    {
        public override WorldGenPass Pass => WorldGenPass.Terrain;
        public override int ChunkSize => 64;

        public override async Task Generate(TileType[,] tiles, WorldGenContext context)
        {
            var worldSize = context.Definitions.World.Size;
            var surfaceLevel = context.Definitions.World.SurfaceLevel;
            var undergroundLevel = context.Definitions.World.UndergroundLevel;

            var heightNoise = CreateNoise(context.Seed, context.Config.Terrain.HeightNoise);
            var heightSpline = CreateSpline(context.Config.Terrain.HeightSpline);
            var stoneOffsetNoise = CreateNoise(context.Seed, context.Config.Terrain.StoneOffsetNoise);

            for (int x = 0; x < worldSize.X; x++)
            {
                var height = heightSpline.Interpolate(heightNoise.Sample1D(x)) + surfaceLevel;
                var stoneLevel = stoneOffsetNoise.Sample1D(x) + undergroundLevel;

                for (int y = 0; y < worldSize.Y; y++)
                {
                    if (y >= height)
                    {
                        tiles[x, y] = y < stoneLevel ? TileType.Dirt : TileType.Stone;
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
