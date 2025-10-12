using System;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;

    private Vector3 initOffset;
    private float CameraBottomY => cam.ScreenToWorldPoint(Vector3.zero).y;

    private void Start() {
        cam = Camera.main?.GetComponent<Camera>();
        if(cam == null) throw new Exception("Could not find camera");
        
        rb = GetComponent<Rigidbody2D>();
        
        Vector3 camBottomPos = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2f, 0, 0));
        initOffset = transform.position - camBottomPos;
        Debug.Log(gameObject.name + ": " + initOffset);
    }

    private void FixedUpdate() {
        rb.MovePosition(initOffset + Vector3.up * (CameraBottomY));
    }
}
