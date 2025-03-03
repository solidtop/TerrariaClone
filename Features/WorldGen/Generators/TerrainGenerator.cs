using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Blocks;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public partial class TerrainGenerator(string description) : WorldGeneratorBase(description)
    {
        private PerlinNoise _heightNoise;
        private CubicSpline _heightSpline;
        private PerlinNoise _stoneOffsetNoise;

        public override void Generate(WorldGenContext context, WorldGenState state, WorldRegion region)
        {
            _heightNoise ??= CreateNoise(context.Seed, context.Config.Terrain.HeightNoise);
            _heightSpline ??= CreateSpline(context.Config.Terrain.HeightSpline);
            _stoneOffsetNoise ??= CreateNoise(context.Seed, context.Config.Terrain.StoneOffsetNoise);

            var surfaceLevel = context.Definitions.World.SurfaceLevel;
            var undergroundLevel = context.Definitions.World.UndergroundLevel;

            for (int x = region.Start.X; x < region.End.X; x++)
            {
                var height = (int)_heightSpline.Interpolate(_heightNoise.Sample1D(x)) + surfaceLevel;
                var stoneLevel = _stoneOffsetNoise.Sample1D(x) + undergroundLevel;

                for (int y = region.Start.Y; y < region.End.Y; y++)
                {
                    if (y >= height)
                    {
                        state.Blocks[x, y] = y < stoneLevel ? BlockType.Dirt : BlockType.Stone;
                    }
                    else
                    {
                        state.Blocks[x, y] = BlockType.Air;
                    }
                }

                state.HeightMap[x] = height;
            }
        }
    }
}
