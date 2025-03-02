using System.Collections.Generic;
using System.Threading.Tasks;
using TerrariaClone.Common.Monitor;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public class WorldInitializer(IEnumerable<IWorldInitializer> initializers, WorldGenContext context, WorldGenState state, IProgressMonitor progressMonitor)
    {
        private readonly List<IWorldInitializer> _initializers = [.. initializers];
        private readonly WorldGenContext _context = context;
        private readonly WorldGenState _state = state;
        private readonly IProgressMonitor _progressMonitor = progressMonitor;

        public async Task InitializeAsync()
        {
            foreach (var initializer in _initializers)
            {
                _progressMonitor.BeginSubTask(initializer.Description);

                await initializer.InitializeAsync(_context, _state);

                _progressMonitor.CompleteTask();
            }
        }

        public int InitializerCount => _initializers.Count;
    }
}
