using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public partial class TerrainGenerator : WorldGeneratorBase
    {
        public override WorldGenPass Pass => WorldGenPass.Terrain;

        private PerlinNoise _heightNoise;
        private CubicSpline _heightSpline;
        private PerlinNoise _stoneOffsetNoise;

        public override void Generate(TileType[,] tiles, TileRegion region, WorldGenContext context)
        {
            var surfaceLevel = context.Definitions.World.SurfaceLevel;
            var undergroundLevel = context.Definitions.World.UndergroundLevel;

            _heightNoise ??= CreateNoise(context.Seed, context.Config.Terrain.HeightNoise);
            _heightSpline ??= CreateSpline(context.Config.Terrain.HeightSpline);
            _stoneOffsetNoise ??= CreateNoise(context.Seed, context.Config.Terrain.StoneOffsetNoise);

            for (int x = region.Start.X; x < region.End.X; x++)
            {
                var height = _heightSpline.Interpolate(_heightNoise.Sample1D(x)) + surfaceLevel;
                var stoneLevel = _stoneOffsetNoise.Sample1D(x) + undergroundLevel;

                for (int y = region.Start.Y; y < region.End.Y; y++)
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
            }
        }
    }
}
