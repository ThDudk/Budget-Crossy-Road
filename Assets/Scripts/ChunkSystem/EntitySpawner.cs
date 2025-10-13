using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using UnityEngine;
using utility;

namespace ChunkSystem {
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField] private ProbabilityMap<GameObject> tilePrefabs;
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float maxSpawnDelay;
        public float MinSpawnDelay => minSpawnDelay;
        public float MaxSpawnDelay => maxSpawnDelay;

        private void Start() {
            StartCoroutine(SpawnCycle());
        }

        private IEnumerator SpawnCycle() {
            while (true) {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
                Spawn();
            }
        }

        
        [CanBeNull]
        public virtual GameObject Spawn() {
            GameObject tile = tilePrefabs.GetRandom();
            if(tile is null) return null;
            
            var tileInstance = Instantiate(tile, transform.parent, true);
            tileInstance.transform.position = transform.position;
            return tileInstance;
        }
    }
}
