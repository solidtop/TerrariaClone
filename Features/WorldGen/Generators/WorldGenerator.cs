using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.Tiles;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Progress;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public class WorldGenerator(IEnumerable<IWorldGenerator> generators, WorldGenContext context, WorldGenState state)
    {
        private readonly List<IWorldGenerator> _generators = generators.ToList();
        private readonly WorldGenContext _context = context;
        private readonly WorldGenState _state = state;
        private readonly Vector2I _worldSize = context.Definitions.World.Size;
        private readonly Vector2I _regionSize = context.Config.World.RegionSize;

        public event Action<WorldGenPass, int> ProgressUpdated;
        public event Action<WorldGenPass, TileType[,]> PassCompleted;
        public event Action<TileType[,]> GenerationCompleted;

        public async Task GenerateAsync()
        {
            foreach (var generator in _generators)
            {
                var tasks = new List<Task>();

                for (int x = 0; x < _worldSize.X; x += _regionSize.X)
                {
                    for (int y = 0; y < _worldSize.Y; y += _regionSize.Y)
                    {
                        var region = new WorldRegion(new Vector2I(x, y), _regionSize);

                        tasks.Add(Task.Run(() =>
                        {
                            generator.Generate(_context, _state, region);
                        }));
                    }
                }

                await Task.WhenAll(tasks);

                PassCompleted?.Invoke(generator.Pass, _state.Tiles);
                UpdateProgress(generator.Pass);
            }

            GenerationCompleted?.Invoke(_state.Tiles);
        }

        private void UpdateProgress(WorldGenPass pass)
        {
            // TODO: Progress shouldn't be based on enum
            var percentage = Mathf.FloorToInt((((float)pass + 1) / _generators.Count) * 100);
            ProgressUpdated?.Invoke(pass, percentage);
        }
    }
}
