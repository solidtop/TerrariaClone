using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TerrariaClone.Features.WorldGen.Contexts;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public class WorldInitializer(IEnumerable<IWorldInitializer> initializers, WorldGenContext context, WorldGenState state)
    {
        private readonly List<IWorldInitializer> _initializers = initializers.ToList();
        private readonly WorldGenContext _context = context;
        private readonly WorldGenState _state = state;

        public async Task InitializeAsync()
        {
            foreach (var initializer in _initializers)
            {
                await initializer.InitializeAsync(_context, _state);
            }
        }
    }
}
