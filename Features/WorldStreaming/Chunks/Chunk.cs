using Godot;
using TerrariaClone.Features.Blocks;

namespace TerrariaClone.Features.WorldStreaming.Chunks
{
    public class Chunk(Vector2I position, Vector2I size, BlockType[,] blocks)
    {
        public Vector2I Position { get; } = position;
        public Vector2I Size { get; } = size;
        public Vector2I PixelSize { get; } = size * Block.Size;
        public BlockType[,] Blocks { get; } = blocks;
    }
}
