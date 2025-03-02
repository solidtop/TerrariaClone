using Godot;

namespace TerrariaClone.Features.WorldStreaming.Pipeline
{
    public class WorldStreamingOptions
    {
        public Vector2I ChunkSize { get; set; }
        public Vector2I StreamDistance { get; set; }
    }
}
