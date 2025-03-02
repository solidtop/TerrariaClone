using System.Collections.Generic;
using System.Linq;
using Godot;

namespace TerrariaClone.Features.WorldStreaming.Chunks.Debug
{
    public partial class ChunkDebugRenderer(IChunkStreamer chunkStreamer) : Node2D
    {
        private readonly IChunkStreamer _chunkStreamer = chunkStreamer;
        private readonly List<Chunk> _chunksToDraw = [];

        public override void _Ready()
        {
            _chunkStreamer.ChunksLoaded += OnChunksLoaded;
            _chunkStreamer.ChunksUnloaded += OnChunksUnloaded;
        }

        public override void _Draw()
        {
            foreach (var chunk in _chunksToDraw)
            {
                var chunkWorldPos = chunk.Position * chunk.PixelSize;
                var rect = new Rect2(chunkWorldPos, chunk.PixelSize);

                DrawRect(rect, Colors.Red, false, 4);
                DrawString(ThemeDB.FallbackFont, chunkWorldPos + new Vector2(8, 32), $"Chunk {chunk.Position}");
            }
        }

        private void OnChunksLoaded(List<Chunk> newChunks)
        {
            _chunksToDraw.AddRange(newChunks);
            QueueRedraw();
        }

        private void OnChunksUnloaded(List<Chunk> oldChunks)
        {
            _chunksToDraw.RemoveAll(chunk => oldChunks.Any(oldChunk => oldChunk.Position == chunk.Position));
            QueueRedraw();
        }
    }
}
