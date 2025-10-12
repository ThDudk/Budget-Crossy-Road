using System.Collections;
using UnityEngine;

namespace ChunkSystem {
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] tilePrefabs;
        [SerializeField] private float minSpawnDelay;
        [SerializeField] private float maxSpawnDelay;

        private void Start() {
            StartCoroutine(SpawnCycle());
        }

        private GameObject GetRandomTile() {
            return tilePrefabs[Random.Range(0, tilePrefabs.Length)];
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
