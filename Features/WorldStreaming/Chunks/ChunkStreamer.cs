using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using TerrariaClone.Features.Blocks;
using TerrariaClone.Features.WorldStreaming.Chunks.Persistence;

namespace TerrariaClone.Features.WorldStreaming.Chunks
{
    public partial class ChunkStreamer(ChunkRepository chunkRepository, Vector2I chunkSize, Vector2I worldSize, Vector2I streamDistance) : IChunkStreamer
    {
        private readonly ChunkRepository _chunkRepository = chunkRepository;
        private readonly Vector2I _chunkSize = chunkSize;
        private readonly Vector2I _worldSize = worldSize;
        private Vector2I _streamDistance = streamDistance;

        private readonly ConcurrentDictionary<Vector2I, Chunk> _loadedChunks = [];

        private int _loadDistance = streamDistance.X * streamDistance.X;
        private int _unloadDistance = (streamDistance.X + UnloadBufferDistance) * (streamDistance.X + UnloadBufferDistance);
        private const int UnloadBufferDistance = 2;

        private Vector2I _lastCameraChunkPosition = new(-1, -1);

        public event Action<List<Chunk>> ChunksLoaded;
        public event Action<List<Chunk>> ChunksUnloaded;

        public async Task StreamAsync(Vector2 cameraPosition)
        {
            var cameraChunkPosition = new Vector2I(
                Mathf.RoundToInt(cameraPosition.X / (_chunkSize.X * Block.Size)),
                Mathf.RoundToInt(cameraPosition.Y / (_chunkSize.Y * Block.Size))
            );

            // Skip update if the camera position hasn't changed
            if (cameraChunkPosition == _lastCameraChunkPosition)
                return;

            _lastCameraChunkPosition = cameraChunkPosition;

            await LoadChunksAsync(cameraChunkPosition);
            await UnloadChunksAsync(cameraChunkPosition);
        }

        public void UpdateStreamDistance(Vector2I streamDistance)
        {
            _streamDistance = streamDistance;

            _loadDistance = streamDistance.X * streamDistance.X;
            _unloadDistance = (streamDistance.X + UnloadBufferDistance) * (streamDistance.X + UnloadBufferDistance);
        }

        private async Task LoadChunksAsync(Vector2I cameraChunkPosition)
        {
            var chunkTasks = new List<Task<Chunk>>();

            for (int xOffset = -_streamDistance.X; xOffset < _streamDistance.X; xOffset++)
            {
                for (int yOffset = -_streamDistance.Y; yOffset < _streamDistance.Y; yOffset++)
                {
                    var chunkPosition = cameraChunkPosition + new Vector2I(xOffset, yOffset);

                    if (!IsChunkWithinBounds(chunkPosition))
                        continue;

                    var chunkDelta = chunkPosition - cameraChunkPosition;
                    var distance = chunkDelta.X * chunkDelta.X + chunkDelta.Y * chunkDelta.Y;

                    if (distance > _loadDistance)
                        continue;

                    if (!IsChunkLoaded(chunkPosition))
                    {
                        chunkTasks.Add(Task.Run(() => LoadChunk(chunkPosition)));
                    }
                }
            }

            var newChunks = await Task.WhenAll(chunkTasks);
            ChunksLoaded?.Invoke([.. newChunks]);
        }

        private async Task UnloadChunksAsync(Vector2I cameraChunkPosition)
        {
            var chunkTasks = new List<Task<Chunk>>();

            foreach (var chunk in _loadedChunks.Values.ToList())
            {
                var chunkDelta = chunk.Position - cameraChunkPosition;
                var distance = chunkDelta.X * chunkDelta.X + chunkDelta.Y * chunkDelta.Y;

                if (distance > _unloadDistance)
                {
                    chunkTasks.Add(Task.Run(() => UnloadChunk(chunk.Position)));
                }
            }

            var oldChunks = await Task.WhenAll(chunkTasks);
            ChunksUnloaded?.Invoke([.. oldChunks]);
        }

        private Chunk LoadChunk(Vector2I chunkPosition)
        {
            var chunk = _chunkRepository.Load(chunkPosition);
            _loadedChunks.TryAdd(chunkPosition, chunk);
            return chunk;
        }

        private Chunk UnloadChunk(Vector2I chunkPosition)
        {
            _loadedChunks.TryRemove(chunkPosition, out var chunk);
            return chunk;
        }

        private bool IsChunkLoaded(Vector2I chunkPosition)
        {
            return _loadedChunks.ContainsKey(chunkPosition);
        }

        private bool IsChunkWithinBounds(Vector2I chunkPosition)
        {
            var maxChunks = _worldSize / _chunkSize;

            return chunkPosition.X >= 0 && chunkPosition.X < maxChunks.X &&
                chunkPosition.Y >= 0 && chunkPosition.Y < maxChunks.Y;
        }
    }
}
