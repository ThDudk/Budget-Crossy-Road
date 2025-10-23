using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static Player.PlayerController;

namespace Player {
    public class PlayerInput : MonoBehaviour {
        private PlayerController controller;
        private InputAction left, right, up, down;
        private Dictionary<InputAction, CardinalDirection> inputToDir = new();
        private InputAction readiedInput;
        private InputAction queuedAction;
        
        public void Start() {
            left = InputSystem.actions.FindAction("Left");
            right = InputSystem.actions.FindAction("Right");
            up = InputSystem.actions.FindAction("Up");
            down = InputSystem.actions.FindAction("Down");

            controller = GetComponent<PlayerController>();

            inputToDir.Add(left, CardinalDirection.Left);
            inputToDir.Add(right, CardinalDirection.Right);
            inputToDir.Add(up, CardinalDirection.Up);
            inputToDir.Add(down, CardinalDirection.Down);
        }

        private void Ready(InputAction action) {
            readiedInput = action;
            controller.Ready(inputToDir[action]);
        }

        private void Update() {
            if (queuedAction == null && GetInput() != null) {
                queuedAction = GetInput();
            }
        }

        private void FixedUpdate() {
            if (queuedAction != null && controller.CanReady()) {
                Ready(queuedAction);
                queuedAction = null;
            }

            if (controller.CanMove() && !readiedInput.IsPressed()) {
                controller.Move();
            }
        }

        [CanBeNull]
        private InputAction GetInput() {
            if(left.WasPressedThisFrame()) return left;
            if(right.WasPressedThisFrame()) return right;
            if(up.WasPressedThisFrame()) return up;
            if(down.WasPressedThisFrame()) return down;
            return null;
        }
    }
}