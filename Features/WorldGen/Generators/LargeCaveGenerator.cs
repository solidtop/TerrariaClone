﻿using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public class LargeCaveGenerator : WorldGeneratorBase
    {
        public override WorldGenPass Pass => WorldGenPass.LargeCaves;

        private PerlinNoise _caveNoise;
        private PerlinNoise _offsetLevelNoise;

        public override void Generate(WorldGenContext context, WorldGenState state, WorldRegion region)
        {
            var undergroundLevel = context.Definitions.World.UndergroundLevel;

            _caveNoise ??= CreateNoise(context.Seed, context.Config.LargeCave.CaveNoise);
            var hollowness = context.Config.LargeCave.CaveNoise.Threshold;

            _offsetLevelNoise ??= CreateNoise(context.Seed, context.Config.LargeCave.OffsetLevelNoise);

            for (int x = region.Start.X; x < region.End.X; x++)
            {
                var offsetLevel = _offsetLevelNoise.Sample1D(x);

                for (int y = region.Start.Y; y < region.End.Y; y++)
                {
                    if (y < undergroundLevel + offsetLevel)
                        continue;

                    var noiseValue = _caveNoise.Sample2D(x, y);

                    if (noiseValue > hollowness)
                    {
                        state.Tiles[x, y] = TileType.Air;
                    }
                }
            }
        }
    }
}
