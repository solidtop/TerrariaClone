namespace TerrariaClone.Features.WorldStreaming.Chunks.Persistence
{
    public interface IChunkWriter
    {
        void Write(Chunk chunk, string path);
    }
}
