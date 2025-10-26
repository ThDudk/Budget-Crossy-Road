using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour {
    private bool moving;
    [SerializeField] public float playerTargetPos;
    [SerializeField] public float catchUpCutoff;
    [SerializeField] private float timeToDieWhenIdle;
    [SerializeField] private float timeToCatchUp;

    public void SetMoving(bool moving) {
        this.moving = moving;
    }
    
    private Transform player;
    private Camera cam;
    
    private Vector3 velocity = Vector3.zero;
    
    private float CameraBottomYPos => cam.ScreenToWorldPoint(Vector3.zero).y;
    private float PlayerPosFromBottom => player.position.y - CameraBottomYPos;
    
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
    }

    private void Update() {
        if (!moving) return;
        
        if (PlayerPosFromBottom > catchUpCutoff) CatchUp();
        else Creep();
    }

    private void CatchUp() {
        var dist = PlayerPosFromBottom - playerTargetPos;
        transform.position += Vector3.SmoothDamp(Vector3.zero, Vector3.up * dist, ref velocity, timeToCatchUp);
    }

    private void Creep() {
        transform.position += Vector3.up * ((playerTargetPos / timeToDieWhenIdle) * Time.deltaTime);
    }
}
