using Entities;
using JetBrains.Annotations;
using UnityEngine;
using utility;

namespace ChunkSystem {
    public class EntitySpawner : MonoBehaviour {
        [SerializeField] private ProbabilityMap<MovingTile> tilePrefabs;
        [SerializeField] public float speed;
        [SerializeField] public float direction;
        
        [CanBeNull]
        public MovingTile Spawn() {
            MovingTile tile = tilePrefabs.GetRandom();
            if(tile is null) return null;
            
            var tileInstance = Instantiate(tile, transform.parent);
            tileInstance.transform.position = transform.position;
            tileInstance.speed = speed;
            tileInstance.direction = direction;
            
            return tileInstance;
        }
    }
}