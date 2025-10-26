using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class Train : MonoBehaviour {
    [SerializeField] private GameObject leftCar;
    [SerializeField] private GameObject middleCar;
    [SerializeField] private GameObject rightCar;
    
    [SerializeField] private int minSize, maxSize;
    public int Size { get; private set; } = -1;
    
    private void Awake() {
        Size = Random.Range(minSize, maxSize);

        if (Size < 2) {
            Destroy(gameObject);
            throw new Exception("Cannot create train of size " + Size);
        }
        
        AddCar(leftCar, 0);
        for (var i = 1; i < Size - 1; i++) AddCar(middleCar, i * 2);
        AddCar(rightCar, (Size - 1) * 2);

        CenterCars();

        var capsule = GetComponent<CapsuleCollider2D>();
        capsule.size = new Vector2(2 * Size - 0.2f, capsule.size.y);
    }
    
    private void AddCar(GameObject car, int offset) {
        var carObj = Instantiate(car, transform);
        carObj.transform.localPosition = Vector3.right * offset;
    }

    private void CenterCars() {
        for (int i = 0; i < transform.childCount; i++) {
            var child = transform.GetChild(i);
            child.localPosition += Vector3.left * (Size - 1);
        }
    }
}
