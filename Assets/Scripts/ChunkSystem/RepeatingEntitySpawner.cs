using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using JetBrains.Annotations;
using UnityEngine;
using utility;

namespace ChunkSystem {
    public class RepeatingEntitySpawner : EntitySpawner
    {
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float maxSpawnDelay;
        [SerializeField] private bool randomInitialOffset = false;
        public float MinSpawnDelay => minSpawnDelay;
        public float MaxSpawnDelay => maxSpawnDelay;

        private void Start() {
            StartCoroutine(SpawnCycle());
        }

        private IEnumerator SpawnCycle() {
            if (randomInitialOffset) {
                yield return new WaitForSeconds(Random.Range(0, maxSpawnDelay));
                Spawn();
            }
            
            while (true) {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
                Spawn();
            }
        }
    }
}
