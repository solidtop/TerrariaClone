using Godot;
using TerrariaClone.Features.Blocks;
using TerrariaClone.Features.WorldGen.Generators;

namespace TerrariaClone.Features.WorldGen.Debug
{
    public partial class WorldGenDebugRenderer(WorldGenerator worldGenerator) : Node2D
    {
        private readonly WorldGenerator _worldGenerator = worldGenerator;

        private BlockType[,] _blocksToDraw;

        public override void _Ready()
        {
            _worldGenerator.GenerationCompleted += OnGenerationCompleted;
        }

        private void OnGenerationCompleted(BlockType[,] blocks)
        {
            _blocksToDraw = blocks;
            QueueRedraw();
        }

        public override void _Draw()
        {
            if (_blocksToDraw is null)
                return;

            var worldWidth = _blocksToDraw.GetLength(0);
            var worldHeight = _blocksToDraw.GetLength(1);

            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    var block = _blocksToDraw[x, y];

                    if (block == BlockType.Air)
                        continue;

                    var blockColor = GetBlockColor(block);

                    var rect = new Rect2(x * Block.Size, y * Block.Size, Block.Size, Block.Size);
                    DrawRect(rect, blockColor, filled: true);
                }
            }
        }

        private static Color GetBlockColor(BlockType block)
        {
            return block switch
            {
                BlockType.Stone => new Color("#888C8D"),
                BlockType.Dirt => new Color("#9b7653"),
                _ => new Color(1f, 1f, 1f),
            };
        }
    }
}
