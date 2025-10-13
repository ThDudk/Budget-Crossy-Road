using ChunkSystem;

namespace DisqualificationRules {
    public class NoRepeatRoadsRule : IDisqualificationRule {
        public bool IsDisqualified(SpawnerContext ctx, Chunk chunk) {
            if (chunk is not RoadChunk) return false;
            
            return ctx.LastChunk is RoadChunk;
        }
    }
}