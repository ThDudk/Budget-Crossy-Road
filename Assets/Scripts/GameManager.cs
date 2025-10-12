using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    private CameraController cam;
    private InputActionMap playerInputs;
    private PlayerController player;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private float Score() => player.transform.position.y - 0;
    
    private IEnumerator Start() {
        cam = Camera.main?.GetComponent<CameraController>();
        if (cam == null) throw new Exception("No Camera Found");
        
        playerInputs = InputSystem.actions.FindActionMap("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
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

    private void Update() {
        scoreText.text = Mathf.Floor(Score()).ToString(CultureInfo.CurrentCulture);
    }
}