using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PauseManager : MonoBehaviour {
    private InputAction pauseAction;
    private InputActionMap playerInputs;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseButton;
    
    private bool isPaused;

    private void Start() {
        pauseAction = InputSystem.actions.FindAction("Pause");
        playerInputs = InputSystem.actions.FindActionMap("Player");
        pauseAction.performed += PausePressed;
        pauseButton.onClick.AddListener(UnPause);
        
        UnPause();
    }

    private void PausePressed(CallbackContext ctx) {
        if (isPaused) {
            UnPause();
            return;
        }
        Pause();
    }

    private void Pause() {
        isPaused = true;
        Time.timeScale = 0;
        playerInputs.Disable();
        pauseMenu.SetActive(true);
    }

    private void UnPause() {
        isPaused = false;
        Time.timeScale = 1;
        playerInputs.Enable();
        pauseMenu.SetActive(false);
    }
}
