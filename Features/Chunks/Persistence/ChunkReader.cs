using Godot;
using TerrariaClone.Common.Serialization;

namespace TerrariaClone.Features.Chunks.Persistence
{
    public class ChunkReader(ISerializer serializer) : IChunkReader
    {
        private readonly ISerializer _serializer = serializer;

        public Chunk Read(string path)
        {
            if (!FileAccess.FileExists(path))
            {
                return null;
            }

            using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
            var data = file.GetBuffer((long)file.GetLength());
            return _serializer.Deserialize<Chunk>(data);
        }
    }
}
