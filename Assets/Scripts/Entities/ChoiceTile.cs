using System.Collections.Generic;
using ChunkSystem;
using UnityEngine;

namespace Entities {
    public class ChoiceTile : RandomTile
    {
        private bool disabled = false;
    
        private void Start() {
            Destroy(gameObject);
            
            if (disabled) return;
            var choiceTiles = DisableAllChoiceTilesInLane();
        
            choiceTiles[Random.Range(0, choiceTiles.Count)].Spawn();
        }
        
        private List<ChoiceTile> DisableAllChoiceTilesInLane() {
            List<ChoiceTile> choiceTiles = new List<ChoiceTile>();
            for (var i = 0; i < transform.parent.childCount; i++) {
                var child = transform.parent.GetChild(i);
                
                ChoiceTile tile;
                if (!child.TryGetComponent(out tile)) continue;
                choiceTiles.Add(tile);
                tile.disabled = true;
            }

            return choiceTiles;
        }
    }
}
