using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerInput : MonoBehaviour {
        private PlayerController controller;

        private InputAction left, right, up, down;
        private Queue<CardinalDirection> moveQueue = new();
        [SerializeField] private int maxQueueItems;
        
        public void Start() {
            left = InputSystem.actions.FindAction("Left");
            right = InputSystem.actions.FindAction("Right");
            up = InputSystem.actions.FindAction("Up");
            down = InputSystem.actions.FindAction("Down");

            controller = GetComponent<PlayerController>();
        }

        public void Update() {
            if (moveQueue.Count > maxQueueItems) return;
            
            if (left.WasPressedThisFrame()) moveQueue.Enqueue(Vector2.left.ToCardinalDirection());
            if (right.WasPressedThisFrame()) moveQueue.Enqueue(Vector2.right.ToCardinalDirection());
            if (up.WasPressedThisFrame()) moveQueue.Enqueue(Vector2.up.ToCardinalDirection());
            if (down.WasPressedThisFrame()) moveQueue.Enqueue(Vector2.down.ToCardinalDirection());
        }

        private void FixedUpdate() {
            if (!moveQueue.Any() || !controller.CanMove()) return;

            controller.TryMove(moveQueue.Dequeue());
        }
    }
}