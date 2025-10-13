using ChunkSystem;

namespace DisqualificationRules {
    public interface IDisqualificationRule {
        public bool IsDisqualified(SpawnerContext ctx, Chunk chunk);
    }
}