using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public class DirtGenerator : WorldGeneratorBase
    {
        public override WorldGenPass Pass => WorldGenPass.Dirt;

        private PerlinNoise _noise;

        public override void Generate(WorldGenContext context, WorldGenState state, WorldRegion region)
        {
            var undergroundLevel = context.Definitions.World.UndergroundLevel;

            _noise ??= CreateNoise(context.Seed, context.Config.Dirt.Noise);
            var threshold = context.Config.Dirt.Noise.Threshold;

            for (int x = region.Start.X; x < region.End.X; x++)
            {
                for (int y = region.Start.Y; y < region.End.Y; y++)
                {
                    if (y < undergroundLevel)
                        continue;

                    var noiseValue = _noise.Sample2D(x, y);

                    if (noiseValue > threshold)
                    {
                        state.Tiles[x, y] = TileType.Dirt;
                    }
                }
            }
        }
    }
}
