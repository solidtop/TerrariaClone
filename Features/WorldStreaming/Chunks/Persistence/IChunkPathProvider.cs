using Godot;

namespace TerrariaClone.Features.WorldStreaming.Chunks.Persistence
{
    public interface IChunkPathProvider
    {
        string GetPath(Vector2I chunkPosition);
    }
}
