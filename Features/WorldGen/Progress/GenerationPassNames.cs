using System.Collections.Generic;

namespace TerrariaClone.Features.WorldGen.Progress
{
    public static class GenerationPassNames
    {
        private static readonly Dictionary<GenerationPass, string> _names = new()
        {
            { GenerationPass.Terrain, "Generating terrain" },
        };

        public static string GetName(GenerationPass pass)
        {
            return _names[pass];
        }
    }
}
