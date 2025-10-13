using System;
using System.Collections.Generic;
using DisqualificationRules;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ChunkSystem {
    public class ChunkSpawner : MonoBehaviour
    {
        [SerializeField] private Chunk[] chunkPrefabs;
        [SerializeField] private float minCameraTopMargin;
        private DisqualificationRuleSet disqualificationRules = DisqualificationRuleSet.AllRules();
        private Dictionary<float, Chunk> chunkInstances = new();
    
        private Camera cam;
        private float CamPadding => transform.position.y - cam.ScreenToWorldPoint(Vector3.up * (Screen.height - 1)).y;
        private float ScreenBottom => cam.ScreenToWorldPoint(Vector3.zero).y;

        private SpawnerContext Context => new(chunkInstances);
    
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
            Chunk chunk = null;
            var iteration = 0;
            while (iteration < 50) {
                iteration++;
                chunk = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
                if (!disqualificationRules.IsDisqualified(Context, chunk)) break;
            }
            SpawnAndMove(chunk);

            if (iteration == 50) throw new Exception("emergency exit triggered. Failed to find valid chunk");
        }

        private void SpawnAndMove(Chunk chunk) {
            var chunkInst = Instantiate(chunk.gameObject, transform.parent);

            Chunk chunkComponent = chunkInst.GetComponent<Chunk>();
            var numLanes = chunkComponent.NumLanes();
            chunkInst.transform.position = transform.position;
        
            chunkInstances[transform.position.y + numLanes] = chunkComponent;
            transform.position += Vector3.up * numLanes;
        }
    }
}
