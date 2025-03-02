using System.Threading.Tasks;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public abstract class WorldInitializerBase(string description) : IWorldInitializer
    {
        public string Description { get; } = description;

        public abstract Task InitializeAsync(WorldGenContext context, WorldGenState state);
    }
}
