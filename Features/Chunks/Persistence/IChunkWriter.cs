namespace TerrariaClone.Features.Chunks.Persistence
{
    public interface IChunkWriter
    {
        void Write(Chunk chunk, string path);
    }
}
