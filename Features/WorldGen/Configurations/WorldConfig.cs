using Godot;
using TerrariaClone.Common.Utilities;

namespace TerrariaClone.Features.WorldGen.Configurations
{
    public class WorldConfig : ConfigLoader<WorldConfig>
    {
        public Vector2I RegionSize { get; set; }
        public Vector2I ChunkSize { get; set; }
    }
}
