using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private Chunk[] chunkPrefabs;

    public void Start() {
        for(var i = 0; i < 10; i++) {
            SpawnAndMove();
        }
    }

    private void SpawnAndMove() {
        Debug.Log("Spawning chunks");
        var chunk = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

        var numLanes = chunk.NumLanes();
        Instantiate(chunk.gameObject, transform.position + Vector3.up * Mathf.Floor(numLanes / 2f), Quaternion.identity);
        transform.position += Vector3.up * numLanes;
    }
}
