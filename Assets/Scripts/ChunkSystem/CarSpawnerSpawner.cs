using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

namespace ChunkSystem {
    public class CarSpawnerSpawner : MonoBehaviour
    {
        [SerializeField] private CarSpawner carPrefab;
        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;

        public void Start() {
            var instance = Instantiate(carPrefab, transform.parent);

            var direction = (Random.value < 0.5f) ? -1 : 1;
            var speed = Random.Range(minSpeed, maxSpeed);

            var spawner = instance.GetComponent<CarSpawner>();
            spawner.direction = direction;
            spawner.speed = speed;

            instance.transform.position = transform.parent.position + Vector3.right * -direction * 9;
            Destroy(gameObject);
        }
    }
}
