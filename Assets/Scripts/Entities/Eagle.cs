using System;
using Player;
using UnityEngine;

public class Eagle : MonoBehaviour {
    private Camera cam;
    private PlayerController player;
    private Rigidbody2D rb;
    
    private const float TopOffset = 5;
    private float ScreenTop => cam.ScreenToWorldPoint(Vector3.up * (Screen.height - 1)).y;
    
    [SerializeField] private float timeToReachPlayer; 
    private float Speed => cam.orthographicSize * 2 / timeToReachPlayer;

    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        var pos = new Vector3(player.transform.position.x, ScreenTop + TopOffset, 0);
        transform.position = pos;
        rb.MovePosition(pos);
        
        rb.linearVelocityY = -Speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.gameObject.CompareTag("Player")) return;

        other.gameObject.SetActive(false);
    }
}
