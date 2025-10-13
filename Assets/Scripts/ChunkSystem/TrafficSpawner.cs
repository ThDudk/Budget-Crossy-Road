using System.Collections;
using NUnit.Framework;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;
using UnityEngine;

namespace ChunkSystem {
    public class TrafficSpawner : MonoBehaviour
    {
        [SerializeField] private CarSpawner carPrefab;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float spawnDistFromCenter = 10;

        public void Start() {
            var instance = Instantiate(carPrefab, transform.parent);

            var direction = (Random.value < 0.5f) ? -1 : 1;
            var speed = Random.Range(minSpeed, maxSpeed);

            var spawner = instance.GetComponent<CarSpawner>();
            spawner.direction = direction;
            spawner.speed = speed;

            instance.transform.position = transform.parent.position + Vector3.right * -direction * spawnDistFromCenter;
            var startTime = Time.time;
            FillWithTraffic(spawner);
            var endTime = Time.time;
            Debug.Log(endTime - startTime);
            
            Destroy(gameObject);
        }

        private void FillWithTraffic(CarSpawner spawner) {
            var dir = spawner.direction;
            var pos = spawner.transform.position;

            while (Vector3.Distance(pos, spawner.transform.position) < spawnDistFromCenter * 2) {
                var delta = Random.Range(spawner.MinSpawnDelay, spawner.MaxSpawnDelay);
                pos.x += spawner.speed * dir * delta;
                
                var car = spawner.Spawn();
                if (car is null) continue;
                
                car.transform.position = pos;
            }
        }
    }
}
