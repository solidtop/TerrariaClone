﻿using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Blocks;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public class DirtGenerator(string description) : WorldGeneratorBase(description)
    {
        private PerlinNoise _noise;

        public override void Generate(WorldGenContext context, WorldGenState state, WorldRegion region)
        {
            _noise ??= CreateNoise(context.Seed, context.Config.Dirt.Noise);

            var threshold = context.Config.Dirt.Noise.Threshold;
            var undergroundLevel = context.Definitions.World.UndergroundLevel;

            for (int x = region.Start.X; x < region.End.X; x++)
            {
                for (int y = region.Start.Y; y < region.End.Y; y++)
                {
                    if (y < undergroundLevel)
                        continue;

                    var noiseValue = _noise.Sample2D(x, y);

                    if (noiseValue > threshold)
                    {
                        state.Blocks[x, y] = BlockType.Dirt;
                    }
                }
            }
        }
    }
}
