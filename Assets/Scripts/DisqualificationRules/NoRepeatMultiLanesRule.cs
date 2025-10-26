using ChunkSystem;

namespace DisqualificationRules {
    public class NoRepeatMultiLanesRule : IDisqualificationRule {
        public bool IsDisqualified(SpawnerContext ctx, Chunk chunk) {
            if (chunk is not MultiLaneChunk) return false;
            
            return ctx.LastChunk is MultiLaneChunk;
        }
    }
}