using System;
using System.Collections.Generic;
using ChunkSystem;

namespace DisqualificationRules {
    [Serializable]
    public class DisqualificationRuleSet : List<IDisqualificationRule> {
        public bool IsDisqualified(SpawnerContext ctx, Chunk chunk) {
            foreach (var rule in this) {
                if (rule.IsDisqualified(ctx, chunk)) {
                    return true;
                }
            }

            return false;
        }
        
        public static DisqualificationRuleSet AllRules() {
            DisqualificationRuleSet set = new DisqualificationRuleSet();
            set.Add(new NoRepeatRoadsRule());
            return set;
        }
    }
}