using Godot;
using TerrariaClone.Common.Utilities;

namespace TerrariaClone.Features.WorldGen.Definitions
{
    public class WorldDefinition : ConfigLoader<WorldDefinition>
    {
        public string Name { get; set; }
        public Vector2I Size { get; set; }
        public int SurfaceLevel { get; set; }
        public int UndergroundLevel { get; set; }
        public int CavernLevel { get; set; }
        public int UnderworldLevel { get; set; }
    }
}
