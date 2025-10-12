using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomTile : MonoBehaviour {
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private int spawnPercentage;

    private void Start() {
        Destroy(gameObject);
        if (Random.Range(0, 100) >= spawnPercentage) return;
        
        var tile = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
        GameObject tileInstance = Instantiate(tile, transform.parent, true);
        tileInstance.transform.position = transform.position;
    }
}
