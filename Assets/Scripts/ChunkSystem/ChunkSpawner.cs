using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private Chunk[] chunkPrefabs;
    [SerializeField] private float minCameraTopMargin;
    private Dictionary<float, GameObject> chunkInstances = new();
    
    private Camera cam;
    private float CamPadding => transform.position.y - cam.ScreenToWorldPoint(Vector3.up * (Screen.height - 1)).y;
    private float screenBottom => cam.ScreenToWorldPoint(Vector3.zero).y;


    private void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        for (int i = 0; i < 3; i++) {
            SpawnAndMove(chunkPrefabs[0]);
        }
    }

    private bool isBelowScreen(float yTopPos) {
        return screenBottom > yTopPos;
    }
    
    public void Update() {
        while (CamPadding < minCameraTopMargin) {
            SpawnAndMove();
        }

        List<float> itemsToRemove = new();
        
        foreach(var topPos in chunkInstances.Keys) {
            if (!isBelowScreen(topPos)) continue;
            
            itemsToRemove.Add(topPos);
        } 
        
        foreach(var item in itemsToRemove) {
            Destroy(chunkInstances[item]);
            chunkInstances.Remove(item);
        }
    }

    private void SpawnAndMove() {
        var chunk = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        SpawnAndMove(chunk);
    }

    private void SpawnAndMove(Chunk chunk) {
        var numLanes = chunk.NumLanes();
        var chunkInst = Instantiate(chunk.gameObject, transform.position + Vector3.up * Mathf.Floor(numLanes / 2f), Quaternion.identity);
        chunkInstances[transform.position.y + chunk.NumLanes()] = chunkInst;
        transform.position += Vector3.up * numLanes;
    }
}
