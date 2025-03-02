using System.Collections.Generic;

namespace TerrariaClone.Features.Tiles
{
    public static class Tile
    {
        public const int Size = 16;

        private static readonly Dictionary<TileType, TileMetadata> Metadata = new()
        {
            { TileType.Dirt, new TileMetadata(new(0, 0)) },
            { TileType.Grass, new TileMetadata(new(1, 0)) },
            { TileType.Stone, new TileMetadata(new(2, 0)) },
        };

        public static TileMetadata GetMetadata(TileType type)
        {
            return Metadata[type];
        }
    }
}
