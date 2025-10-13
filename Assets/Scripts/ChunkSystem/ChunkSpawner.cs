using System;
using System.Collections;
using System.Collections.Generic;
using ChunkSystem;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private Chunk[] chunkPrefabs;
    [SerializeField] private float minCameraTopMargin;
    private Dictionary<float, GameObject> chunkInstances = new();
    
    private Camera cam;
    private float CamPadding => transform.position.y - cam.ScreenToWorldPoint(Vector3.up * (Screen.height - 1)).y;
    private float ScreenBottom => cam.ScreenToWorldPoint(Vector3.zero).y;


    private void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        for (int i = 0; i < 3; i++) {
            SpawnAndMove(chunkPrefabs[0]);
        }
    }

    private bool IsBelowScreen(float yTopPos) {
        return ScreenBottom > yTopPos;
    }
    
    public void Update() {
        while (CamPadding < minCameraTopMargin) {
            SpawnAndMove();
        }

        List<float> itemsToRemove = new();
        
        foreach(var topPos in chunkInstances.Keys) {
            if (!IsBelowScreen(topPos)) continue;
            
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
        var chunkInst = Instantiate(chunk.gameObject, transform.parent);
        
        var numLanes = chunkInst.GetComponent<Chunk>().NumLanes();
        chunkInst.transform.position = transform.position;
        
        chunkInstances[transform.position.y + numLanes] = chunkInst;
        transform.position += Vector3.up * numLanes;
    }
}
