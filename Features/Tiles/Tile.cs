using System.Collections.Generic;

namespace TerrariaClone.Features.Tiles
{
    public static class Tile
    {
        public const int Size = 16;

        private static readonly Dictionary<TileType, TileMetadata> Metadata = new()
        {
            { TileType.Stone, new TileMetadata(0) },
            { TileType.Dirt, new TileMetadata(1) },
            { TileType.Grass, new TileMetadata(2) },
        };

        public static TileMetadata GetMetadata(TileType type)
        {
            return Metadata[type];
        }
    }
}
