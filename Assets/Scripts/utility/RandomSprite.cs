using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSprite : MonoBehaviour {
    [SerializeField] private Sprite[] textures;

    public void Start() {
        GetComponent<SpriteRenderer>().sprite = textures[Random.Range(0, textures.Length)];
    }
}
