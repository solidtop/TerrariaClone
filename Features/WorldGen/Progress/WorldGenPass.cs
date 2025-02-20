using System.Collections.Generic;

namespace TerrariaClone.Features.WorldGen.Progress
{
    public enum WorldGenPass
    {
        Terrain,
        Stone,
        Dirt,
        SmallCaves,
        LargeCaves,
    }

    public static class WorldGenPassInfo
    {
        private static readonly Dictionary<WorldGenPass, string> _descriptions = new()
        {
            { WorldGenPass.Terrain, "Generating terrain" },
            { WorldGenPass.Stone, "Placing stone in the dirt" },
            { WorldGenPass.Dirt, "Placing dirt in the stone" },
            { WorldGenPass.SmallCaves, "Generating small caves" },
            { WorldGenPass.LargeCaves, "Generating large caves" },
        };

        public static string GetDescription(WorldGenPass pass)
        {
            return _descriptions[pass];
        }
    }
}
