namespace TerrariaClone.Features.Chunks.Persistence
{
    public interface IChunkReader
    {
        Chunk Read(string path);
    }
}
