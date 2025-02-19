using System.Collections.Generic;
using Godot;

namespace TerrariaClone.Features.Chunks.Debug
{
    public partial class ChunkDebugRenderer(IChunkStreamer chunkStreamer) : Node2D
    {
        private readonly IChunkStreamer _chunkStreamer = chunkStreamer;
        private List<Chunk> _chunksToDraw = [];

        public override void _Ready()
        {
            _chunkStreamer.ChunksLoaded += OnChunksLoaded;
        }

        public override void _Draw()
        {
            foreach (var chunk in _chunksToDraw)
            {
                var chunkWorldPos = chunk.Position * chunk.PixelSize;
                var rect = new Rect2(chunkWorldPos, chunk.Size);

                DrawRect(rect, Colors.Red, false, 2);
            }
        }

        private void OnChunksLoaded(List<Chunk> chunks)
        {
            _chunksToDraw = chunks;
            QueueRedraw();
        }
    }
}
