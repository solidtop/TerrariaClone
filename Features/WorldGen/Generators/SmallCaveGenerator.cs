﻿using TerrariaClone.Common.Utilities;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Generators
{
    internal class SmallCaveGenerator : WorldGeneratorBase
    {
        public override WorldGenPass Pass => WorldGenPass.SmallCaves;

        private PerlinNoise _caveNoise;
        private PerlinNoise _offsetLevelNoise;

        public override void Generate(WorldGenContext context, WorldGenState state, WorldRegion region)
        {
            var surfaceLevel = context.Definitions.World.SurfaceLevel;
            var undergroundLevel = context.Definitions.World.UndergroundLevel;

            _caveNoise ??= CreateNoise(context.Seed, context.Config.SmallCave.CaveNoise);
            var hollowness = context.Config.SmallCave.CaveNoise.Threshold;

            _offsetLevelNoise ??= CreateNoise(context.Seed, context.Config.SmallCave.OffsetLevelNoise);

            for (int x = region.Start.X; x < region.End.X; x++)
            {
                var offsetLevel = (_offsetLevelNoise.Sample1D(x) + 1) * 0.5f;

                for (int y = region.Start.Y; y < region.End.Y; y++)
                {
                    if (y < surfaceLevel + offsetLevel || y >= undergroundLevel + offsetLevel)
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
