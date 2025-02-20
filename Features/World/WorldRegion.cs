using Godot;

namespace TerrariaClone.Features.World
{
    public class WorldRegion(Vector2I position, Vector2I size)
    {
        public Vector2I Start { get; } = position;
        public Vector2I End { get; } = position + size;
    }
}
