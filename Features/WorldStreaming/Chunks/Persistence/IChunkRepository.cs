using Godot;

namespace TerrariaClone.Features.WorldStreaming.Chunks.Persistence
{
    public interface IChunkRepository
    {
        void Save(Chunk chunk);
        Chunk Load(Vector2I position);
    }
}
