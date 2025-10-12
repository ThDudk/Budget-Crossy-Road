using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace ChunkSystem {
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<GameObject, float> tilePrefabs;
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float maxSpawnDelay;

        private void Start() {
            StartCoroutine(SpawnCycle());
        }

        private GameObject GetRandomTile() {
            float value = Random.value;
            GameObject chosenTile = null;
            foreach(KeyValuePair<GameObject, float> pair in tilePrefabs)
            {
                if (pair.Value > value) continue;

                if (chosenTile is null || pair.Value > tilePrefabs[chosenTile]) {
                    chosenTile = pair.Key;
                }
            }
            
            return chosenTile;
        }

        private IEnumerator SpawnCycle() {
            while (true) {
                yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
                Spawn();
            }
        }

        protected virtual GameObject Spawn() {
            var tileInstance = Instantiate(GetRandomTile(), transform.parent, true);
            tileInstance.transform.position = transform.position;
            return tileInstance;
        }
    }
}
