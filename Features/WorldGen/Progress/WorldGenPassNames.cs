using System.Collections.Generic;

namespace TerrariaClone.Features.WorldGen.Progress
{
    public static class WorldGenPassNames
    {
        private static readonly Dictionary<WorldGenPass, string> _names = new()
        {
            { WorldGenPass.Terrain, "Generating terrain" },
        };

        public static string GetName(WorldGenPass pass)
        {
            return _names[pass];
        }
    }
}
