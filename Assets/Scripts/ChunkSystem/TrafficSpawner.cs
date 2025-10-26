using System.Collections;
using NUnit.Framework;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChunkSystem {
    public class TrafficSpawner : MonoBehaviour
    {
        [SerializeField] private RepeatingEntitySpawner carSpawner;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float spawnDistFromCenter = 10;
        [SerializeField] private bool fillOnStart = true;
 
        public void Start() {
            var instance = Instantiate(carSpawner, transform.parent);

            var direction = (Random.value < 0.5f) ? -1 : 1;
            var speed = Random.Range(minSpeed, maxSpeed);

            var spawner = instance.GetComponent<RepeatingEntitySpawner>();
            spawner.direction = direction;
            spawner.speed = speed;

            instance.transform.position = transform.parent.position + Vector3.right * -direction * spawnDistFromCenter;
            if(fillOnStart) FillWithTraffic(spawner);
            
            Destroy(gameObject);
        }

        private void FillWithTraffic(RepeatingEntitySpawner spawner) {
            var dir = spawner.direction;
            var pos = spawner.transform.position;

            while (Vector3.Distance(pos, spawner.transform.position) < spawnDistFromCenter * 2) {
                var car = spawner.Spawn();

                if (car != null) car.transform.position = pos;

                var delta = Random.Range(spawner.MinSpawnDelay, spawner.MaxSpawnDelay);
                pos.x += spawner.speed * dir * delta;
            }
        }
    }
}
