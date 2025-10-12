using Unity.VisualScripting;
using UnityEngine;

namespace ChunkSystem {
    public class CarSpawner : EntitySpawner {
        [SerializeField] public float speed;
        [SerializeField] public float direction;
        
        protected override GameObject Spawn() {
            var tile = base.Spawn();
            if (tile is null) return null;
            
            MovingTile car = tile.GetComponent<MovingTile>();
            car.direction = direction;
            car.speed = speed;
            
            return tile;
        }
    }
}
