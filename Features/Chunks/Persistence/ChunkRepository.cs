using System.IO;
using Godot;

namespace TerrariaClone.Features.Chunks.Persistence
{
    public class ChunkRepository(IChunkReader chunkReader, IChunkWriter chunkWriter, string basePath) : IChunkRepository
    {
        private readonly IChunkReader _chunkReader = chunkReader;
        private readonly IChunkWriter _chunkWriter = chunkWriter;
        private readonly string _basePath = basePath;

        public void Save(Chunk chunk)
        {
            var filePath = GetFilePath(chunk.Position);
            _chunkWriter.Write(chunk, filePath);
        }

        public Chunk Load(Vector2I position)
        {
            var filePath = GetFilePath(position);
            return _chunkReader.Read(filePath);
        }

        private string GetFilePath(Vector2I position)
        {
            return Path.Combine(_basePath, $"chunk_{position.X}_{position.Y}.save");
        }
    }
}
