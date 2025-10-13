using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovingTile : MonoBehaviour {
    public float speed = 0;
    public float direction = 0;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void FixedUpdate() {
        rb.linearVelocity = Vector2.right * (speed * direction);
        sr.flipX = direction > 0;
        
        if(Mathf.Abs(transform.position.x) > 10) Destroy(gameObject);
    }
}
