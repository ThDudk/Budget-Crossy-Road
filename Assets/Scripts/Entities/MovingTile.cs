using Unity.VisualScripting;
using UnityEngine;

namespace Entities {
    public class MovingTile : MonoBehaviour {
        public float speed = 0;
        public float direction = 0;
        [SerializeField] private bool flipSprite = false;
        private Rigidbody2D rb;
        private Collider2D coll;
        private SpriteRenderer sr;

        [SerializeField] private float moveDelay;
        private float creationTime;
        private float TimeSinceCreation => Time.time - creationTime;
        
        private float Width => coll.bounds.size.x;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponentInChildren<SpriteRenderer>();
            coll = GetComponent<Collider2D>();
            
            creationTime = Time.time;
        }

        public void FixedUpdate() {
            if (TimeSinceCreation < moveDelay) return;
            
            rb.linearVelocity = Vector2.right * (speed * direction);
            if(flipSprite) sr.flipX = direction > 0;
        
            if(transform.position.x * direction > 10 + Width/2) Destroy(gameObject);
        }
    }
}
