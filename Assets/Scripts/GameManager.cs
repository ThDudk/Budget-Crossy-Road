using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    private CameraController cam;
    private InputActionMap playerInputs;
    
    private IEnumerator Start() {
        cam = Camera.main?.GetComponent<CameraController>();
        playerInputs = InputSystem.actions.FindActionMap("Player");
        if (cam == null) throw new Exception("No Camera Found");
        cam.SetMoving(false);

        yield return StartCoroutine(WaitUntilActionPressed());
        
        cam.SetMoving(true);
    }

    private IEnumerator WaitUntilActionPressed() {
        bool actionPressed = false;
        
        void OnAnyActionPerformed(InputAction.CallbackContext _) => actionPressed = true;

        foreach (var action in playerInputs) {
            action.performed += OnAnyActionPerformed;
        }
        
        yield return new WaitUntil(() => actionPressed);
        
        foreach (var action in playerInputs) {
            action.performed -= OnAnyActionPerformed;
        }
    }
}