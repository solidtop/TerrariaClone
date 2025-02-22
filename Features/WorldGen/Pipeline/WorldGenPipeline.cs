using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.WorldGen.Debug;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Initializers;

namespace TerrariaClone.Features.WorldGen.Pipeline
{
    public class WorldGenPipeline(WorldGenerator generator, WorldInitializer initializer, IEnumerable<IWorldGenDebugger> debuggers)
    {
        private readonly WorldGenerator _generator = generator;
        private readonly WorldInitializer _initializer = initializer;
        private readonly List<IWorldGenDebugger> _debuggers = debuggers.ToList();

        public async Task RunAsync()
        {
            await _generator.GenerateAsync();
            await _initializer.InitializeAsync();
        }

        public async Task RunAsync(Node rootNode)
        {
            foreach (var debugger in _debuggers)
            {
                debugger.SetWorldGenerator(_generator);

                if (debugger is Node node)
                {
                    rootNode.AddChild(node);
                }
            }

            await RunAsync();
        }
    }
}
