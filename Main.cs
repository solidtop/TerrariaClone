using Godot;
using TerrariaClone.Features.WorldGen.Configurations;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Debug;
using TerrariaClone.Features.WorldGen.Definitions;
using TerrariaClone.Features.WorldGen.Generators;

namespace TerrariaClone;

public partial class Main : Node
{
	public override void _Ready()
	{
		var seed = 12345;

		var definitions = WorldDefinitions.Load("./Data/Worlds/TestWorld/WorldGen");
		var config = WorldGenConfig.Load("./Data/Worlds/TestWorld/WorldGen");

        var context = new WorldGenContext(seed, definitions, config);

        var worldGenerator = new WorldGenerator([
            new TerrainGenerator(),
            new StoneGenerator(),
            new DirtGenerator(),
        ], context);

        var debugRenderer = new WorldGenDebugRenderer(worldGenerator);
        AddChild(debugRenderer);

        _ = worldGenerator.Generate();
    }
}
