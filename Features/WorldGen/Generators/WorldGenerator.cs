using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Common.Monitor;
using TerrariaClone.Features.Blocks;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Generators
{
    public class WorldGenerator(IEnumerable<IWorldGenerator> generators, WorldGenContext context, WorldGenState state, IProgressMonitor progressMonitor)
    {
        private readonly List<IWorldGenerator> _generators = [.. generators];
        private readonly WorldGenContext _context = context;
        private readonly WorldGenState _state = state;
        private readonly IProgressMonitor _progressMonitor = progressMonitor;

        private readonly Vector2I _worldSize = context.Definitions.World.Size;
        private readonly Vector2I _regionSize = context.Config.RegionSize;

        public event Action<int, string> ProgressUpdated;
        public event Action<BlockType[,]> GenerationCompleted;

        public async Task GenerateAsync()
        {
            foreach (var generator in _generators)
            {
                _progressMonitor.BeginSubTask(generator.Description);

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

                _progressMonitor.CompleteTask();
            }

            GenerationCompleted?.Invoke(_state.Blocks);
        }

        public int GeneratorCount => _generators.Count;
    }
}
