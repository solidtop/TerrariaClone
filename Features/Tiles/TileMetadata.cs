using Godot;

namespace TerrariaClone.Features.Tiles
{
    public readonly struct TileMetadata(Vector2I atlasCoords)
    {
        public Vector2I AtlasCoords { get; } = atlasCoords;
    }
}
