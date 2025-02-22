using System.Threading.Tasks;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.State;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public interface IWorldInitializer
    {
        Task InitializeAsync(WorldGenContext context, WorldGenState state);
    }
}
