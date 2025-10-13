using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerInput : MonoBehaviour {
        private PlayerController controller;

        private class QueueItem : Tuple<float, CardinalDirection> {
            public QueueItem(CardinalDirection item2) : base(Time.unscaledTime, item2) {}

            public float TimeSince => Time.unscaledTime - TimeStamp;
            public float TimeStamp => Item1;
            public CardinalDirection Direction => Item2;

            public override string ToString() {
                return "(" + TimeStamp + " : " + Direction + " - " + TimeSince + ")"; 
            }
        }

        private InputAction left, right, up, down;
        [SerializeField] private float inputDropTime;
        private readonly Queue<QueueItem> inputQueue = new();
        
        public void Start() {
            left = InputSystem.actions.FindAction("Left");
            right = InputSystem.actions.FindAction("Right");
            up = InputSystem.actions.FindAction("Up");
            down = InputSystem.actions.FindAction("Down");

            controller = GetComponent<PlayerController>();
        }

        public void Update() {
            if (left.WasPressedThisFrame()) inputQueue.Enqueue(new(Vector2.left.ToCardinalDirection()));
            if (right.WasPressedThisFrame()) inputQueue.Enqueue(new(Vector2.right.ToCardinalDirection()));
            if (up.WasPressedThisFrame()) inputQueue.Enqueue(new(Vector2.up.ToCardinalDirection()));
            if (down.WasPressedThisFrame()) inputQueue.Enqueue(new(Vector2.down.ToCardinalDirection()));
        }

        private void FixedUpdate() {
            if (!inputQueue.Any() || !controller.CanMove()) return;

            while (inputQueue.Count > 0) {
                var item = inputQueue.Dequeue();
                if (item.TimeSince > inputDropTime) continue;
                
                controller.TryMove(item.Direction);
                return;
            }
        }
    }
}