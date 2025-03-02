using Godot;

namespace TerrariaClone.Features.WorldStreaming.Chunks.Persistence
{
    public class ChunkRepository(IChunkReader chunkReader, IChunkWriter chunkWriter, IChunkPathProvider pathProvider) : IChunkRepository
    {
        private readonly IChunkReader _chunkReader = chunkReader;
        private readonly IChunkWriter _chunkWriter = chunkWriter;
        private readonly IChunkPathProvider _pathProvider = pathProvider;

        public void Save(Chunk chunk)
        {
            var filePath = _pathProvider.GetPath(chunk.Position);
            _chunkWriter.Write(chunk, filePath);
        }

        public Chunk Load(Vector2I position)
        {
            var filePath = _pathProvider.GetPath(position);
            return _chunkReader.Read(filePath);
        }
    }
}
