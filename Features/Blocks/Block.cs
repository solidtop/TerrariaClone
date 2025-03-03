using System.Collections.Generic;

namespace TerrariaClone.Features.Blocks
{
    public static class Block
    {
        public const int Size = 16;

        private static readonly Dictionary<BlockType, BlockMetadata> Metadata = new()
        {
            { BlockType.Dirt, new BlockMetadata(new(0, 0)) },
            { BlockType.Grass, new BlockMetadata(new(1, 0)) },
            { BlockType.Stone, new BlockMetadata(new(2, 0)) },
        };

        public static BlockMetadata GetMetadata(BlockType block)
        {
            return Metadata[block];
        }
    }
}
