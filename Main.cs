using Godot;
using TerrariaClone.Common.Serialization;
using TerrariaClone.Features.Chunks.Persistence;
using TerrariaClone.Features.World;
using TerrariaClone.Features.WorldGen;
using TerrariaClone.Features.WorldGen.Configurations;
using TerrariaClone.Features.WorldGen.Contexts;
using TerrariaClone.Features.WorldGen.Debug;
using TerrariaClone.Features.WorldGen.Definitions;
using TerrariaClone.Features.WorldGen.Generators;
using TerrariaClone.Features.WorldGen.Initializers;

namespace TerrariaClone;

public partial class Main : Node
{
    public override async void _Ready()
    {
        var seed = 12345;

        var definitions = WorldDefinitions.Load("./Data/Worlds/TestWorld/WorldGen");
        var config = WorldGenConfig.Load("./Data/Worlds/TestWorld/WorldGen");

        var context = new WorldGenContext(seed, definitions, config);
        var state = new WorldGenState(definitions.World.Size);

        var worldGenerator = new WorldGenerator([
            new TerrainGenerator(),
            new StoneGenerator(),
            new DirtGenerator(),
            new SmallCaveGenerator(),
            new LargeCaveGenerator(),
        ], context, state);

        //var debugRenderer = new WorldGenDebugRenderer(worldGenerator);
        //AddChild(debugRenderer);

        var progressReporter = new WorldGenProgressReporter(worldGenerator);
        AddChild(progressReporter);

        //var tileRenderer = new TileRenderer(worldGenerator);
        //AddChild(tileRenderer);

        var worldInitializer = new WorldInitializer([
            new PartitionInitializer(new WorldPartitioner()),
            new SaveInitializer(new ChunkWriter(new MessagePackAdapter()), "user://"),
        ], context, state);

        await worldGenerator.GenerateAsync();
        await worldInitializer.InitializeAsync();
    }

    //public override void _Ready()
    //{
    //    var messagePackAdapter = new MessagePackAdapter();
    //    var chunkReader = new ChunkReader(messagePackAdapter);
    //    var chunkWriter = new ChunkWriter(messagePackAdapter);
    //    var chunkRepository = new ChunkRepository(chunkReader, chunkWriter, "user://");
    //    var chunkStreamer = new ChunkStreamer(chunkRepository, new Vector2I(16, 16), new Vector2I(5, 3));

    //    var worldStreamer = new WorldStreamer(chunkStreamer);
    //    AddChild(worldStreamer);

    //    var chunkDebugRenderer = new ChunkDebugRenderer(chunkStreamer);
    //    AddChild(chunkDebugRenderer);
    //}
}
