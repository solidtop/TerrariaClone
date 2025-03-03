using Godot;

namespace TerrariaClone.Features.Blocks
{
    public readonly struct BlockMetadata(Vector2I atlasCoords)
    {
        public Vector2I AtlasCoords { get; } = atlasCoords;
    }
}
