using Godot;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Debug
{
    public partial class WorldGenDebugRenderer(WorldGenerator worldGenerator) : Node2D
    {
        private readonly WorldGenerator _worldGenerator = worldGenerator;

        private TileType[,] _tiles;
        private WorldGenPass _currentPass;

        public override void _Ready()
        {
            _worldGenerator.GenerationCompleted += Render;
            _worldGenerator.ProgressUpdated += PrintProgress;
        }
      
        private void Render(TileType[,] tiles)
        {
            _tiles = tiles;
            QueueRedraw();
            GD.Print("Rendered tiles: 100%");
            GD.Print("Generation completed");
        }

        public override void _Draw()
        {
            if (_tiles is null) return;

            var worldWidth = _tiles.GetLength(0);
            var worldHeight = _tiles.GetLength(1);

            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    var tile = _tiles[x, y];

                    if (tile == TileType.Air)
                        continue;

                    var tileColor = GetTileColor(tile);

                    var rect = new Rect2(x * Tile.Size, y * Tile.Size, Tile.Size, Tile.Size);
                    DrawRect(rect, tileColor, filled: true);
                }
            }
        }

        private static Color GetTileColor(TileType tile)
        {
            return tile switch
            {
                TileType.Stone => new Color("#888C8D"),
                TileType.Dirt => new Color("#9b7653"), 
                _ => new Color(1f, 1f, 1f),
            };
        }

        private static void PrintProgress(WorldGenPass pass, int percentage)
        {
            GD.Print($"{WorldGenPassInfo.GetDescription(pass)}: {percentage}%");
        }  
    }
}
