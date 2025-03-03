using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Blocks;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public class StoneGenerator(string description) : WorldGeneratorBase(description)
    {
        private PerlinNoise _noise;

        public override void Generate(WorldGenContext context, WorldGenState state, WorldRegion region)
        {
            _noise ??= CreateNoise(context.Seed, context.Config.Stone.Noise);

            var threshold = context.Config.Stone.Noise.Threshold;

            for (int x = region.Start.X; x < region.End.X; x++)
            {
                for (int y = region.Start.Y; y < region.End.Y; y++)
                {
                    if (state.Blocks[x, y] != BlockType.Dirt)
                        continue;

                    var noiseValue = _noise.Sample2D(x, y);

                    if (noiseValue > threshold)
                    {
                        state.Blocks[x, y] = BlockType.Stone;
                    }
                }
            }
        }
    }
}
