using System.Collections.Generic;
using System.Linq;
using ChunkSystem;
using UnityEngine;

namespace DisqualificationRules {
    public class SpawnerContext {
        public Dictionary<float, Chunk> ChunkInstances { get; }

        public SpawnerContext(Dictionary<float, Chunk> chunkInstances) {
            ChunkInstances = chunkInstances;
        }

        public Chunk LastChunk => ChunkInstances[ChunkInstances.Keys.Max()];
    }
}