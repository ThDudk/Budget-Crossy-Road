using UnityEngine;

namespace utility {
    public class RandomSprite : MonoBehaviour {
        [SerializeField] private ProbabilityMap<Sprite> textures;

        public void Start() {
            GetComponent<SpriteRenderer>().sprite = textures.GetRandom();
        }
    }
}
