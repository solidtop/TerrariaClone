namespace TerrariaClone.Features.WorldStreaming.Chunks.Persistence
{
    public interface IChunkReader
    {
        Chunk Read(string path);
    }
}
