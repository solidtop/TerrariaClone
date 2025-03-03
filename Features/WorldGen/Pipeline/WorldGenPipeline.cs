using System.Threading.Tasks;
using TerrariaClone.Common.Monitor;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Initializers;

namespace TerrariaClone.Features.WorldGen.Pipeline
{
    public class WorldGenPipeline(WorldGenerator generator, WorldInitializer initializer, IProgressMonitor progressMonitor)
    {
        private readonly WorldGenerator _generator = generator;
        private readonly WorldInitializer _initializer = initializer;

        public WorldGenerator WorldGenerator => _generator;
        public IProgressMonitor ProgressMonitor { get; } = progressMonitor;

        public async Task RunAsync()
        {
            var subTasks = _generator.GeneratorCount + _initializer.InitializerCount;
            ProgressMonitor.BeginTask("World generation", subTasks);

            await _generator.GenerateAsync();
            await _initializer.InitializeAsync();

            ProgressMonitor.CompleteTask();
        }
    }
}
