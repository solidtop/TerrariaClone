using Godot;
using TerrariaClone.Features.Tiles;

namespace TerrariaClone.Features.WorldStreaming.Chunks
{
    public class Chunk(Vector2I position, Vector2I size, TileType[,] tiles)
    {
        public Vector2I Position { get; } = position;
        public Vector2I Size { get; } = size;
        public Vector2I PixelSize { get; } = size * Tile.Size;
        public TileType[,] Tiles { get; } = tiles;
    }
}
