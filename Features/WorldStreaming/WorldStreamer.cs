using Godot;
using TerrariaClone.Features.WorldStreaming.Chunks;

namespace TerrariaClone.Features.WorldStreaming
{
    public partial class WorldStreamer(IChunkStreamer chunkStreamer) : Node
    {
        private readonly IChunkStreamer _chunkStreamer = chunkStreamer;

        private Camera2D _camera;

        public IChunkStreamer ChunkStreamer => _chunkStreamer;

        public override void _Ready()
        {
            _camera = GetViewport().GetCamera2D();
        }

        public override void _Process(double delta)
        {
            var cameraPosition = _camera.GlobalPosition;
            _chunkStreamer.StreamAsync(cameraPosition);
        }
    }
}
