using System.Threading.Tasks;
using TerrariaClone.Features.WorldGen.Contexts;

namespace TerrariaClone.Features.WorldGen.Initializers
{
    public interface IWorldInitializer
    {
        Task InitializeAsync(WorldGenContext context, WorldGenState state);
    }
}
