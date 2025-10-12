using System;
using UnityEngine;
using utility;
using Random = UnityEngine.Random;

public class RandomTile : MonoBehaviour {
    [SerializeField] private ProbabilityMap<GameObject> tilePrefabs;

    private void Start() {
        Destroy(gameObject);
        
        var tile = tilePrefabs.GetRandom();
        if (tile is null) return;
        
        GameObject tileInstance = Instantiate(tile, transform.parent, true);
        tileInstance.transform.position = transform.position;
    }
}
