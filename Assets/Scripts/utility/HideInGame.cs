using UnityEngine;

public class HideInGame : MonoBehaviour
{
    void Start() {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
