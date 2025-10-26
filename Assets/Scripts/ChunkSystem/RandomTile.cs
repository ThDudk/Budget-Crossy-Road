using JetBrains.Annotations;
using UnityEngine;
using utility;

namespace ChunkSystem {
    public class RandomTile : MonoBehaviour {
        [SerializeField] private ProbabilityMap<GameObject> tilePrefabs;

        private void Start() {
            Destroy(gameObject);
        
            Spawn();
        }

        [CanBeNull]
        protected GameObject Spawn() {
            var tile = tilePrefabs.GetRandom();
            if (tile is null) return null;
        
            GameObject tileInstance = Instantiate(tile, transform.parent, true);
            tileInstance.transform.position = transform.position;
            return tileInstance;
        }
    }
}
