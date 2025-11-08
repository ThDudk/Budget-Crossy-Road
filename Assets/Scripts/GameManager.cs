using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private CameraController cam;
    private InputActionMap playerInputs;
    private PlayerController player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private float highestPlayerPos;
    private int Score() => (int) Mathf.Floor(Mathf.Max(0, highestPlayerPos));
    
    private IEnumerator Start() {
        cam = Camera.main?.GetComponent<CameraController>();
        if (cam == null) throw new Exception("No Camera Found");
        
        playerInputs = InputSystem.actions.FindActionMap("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        cam.SetMoving(false);
        
        highScoreText.text = "High Score: " + SaveSystem.Load();
        highScoreText.gameObject.SetActive(true);

        yield return StartCoroutine(WaitUntilActionPressed());
        
        highScoreText.gameObject.SetActive(false);
        
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
        if(player.transform.position.y > highestPlayerPos) highestPlayerPos = player.transform.position.y;
        scoreText.text = Score().ToString(CultureInfo.CurrentCulture);
    }

    public void Death() {
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine() {
        cam.SetMoving(false);
        if(Score() > SaveSystem.Load()) SaveSystem.Save(Score());
        player.DisableMovement();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}