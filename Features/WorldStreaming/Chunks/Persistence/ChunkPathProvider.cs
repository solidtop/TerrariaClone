using System.IO;
using Godot;

namespace TerrariaClone.Features.WorldStreaming.Chunks.Persistence
{
    public class ChunkPathProvider(string basePath) : IChunkPathProvider
    {
        private readonly string _basePath = basePath;

        public string GetPath(Vector2I chunkPosition)
        {
            return Path.Combine(_basePath, $"chunk_{chunkPosition.X}_{chunkPosition.Y}.save");
        }
    }
}
