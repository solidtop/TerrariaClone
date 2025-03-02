using Godot;

namespace TerrariaClone.Features.WorldGen.Pipeline
{
    public class WorldGenOptions
    {
        public int Seed { get; set; }
        public string ConfigPath { get; set; }
        public Vector2I RegionSize { get; set; }
        public Vector2I ChunkSize { get; set; }
    }
}
