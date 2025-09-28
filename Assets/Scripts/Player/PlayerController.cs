using System;
using System.Collections;
using UnityEngine;

namespace Player {
    public class PlayerController : MonoBehaviour {
        private Rigidbody2D rb;
        [SerializeField] private float moveSecs;
        [SerializeField] private float readjustSpeed;
        private enum States {Idle, Moving, Readjusting}
        private Coroutine currentRoutine;

        private States state = States.Idle;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        public bool CanMove() => state == States.Idle;

        public void TryMove(CardinalDirection direction) {
            if (!CanMove()) return;
        
            currentRoutine = StartCoroutine(Move(direction));
        }

        public void OnCollisionEnter2D(Collision2D other) {
            if(state == States.Readjusting) throw new Exception("Collision entered while readjusting");
            
            StopCoroutine(currentRoutine);
            StartCoroutine(Readjust(startPos));
        }

        private Vector3 startPos;
        private IEnumerator Move(CardinalDirection direction) {
            state = States.Moving;
            float elapsedTime = 0;

            startPos = transform.position;
            var endPos = transform.position + direction.ToVector3();

            while (elapsedTime < moveSecs) {
                elapsedTime += Time.fixedDeltaTime;
                rb.MovePosition(Vector3.Lerp(startPos, endPos, Mathf.Min(1, elapsedTime / moveSecs)));
                yield return new WaitForFixedUpdate();
            }

            StartCoroutine(Readjust(endPos));
        }
        
        private IEnumerator Readjust(Vector3 pos) {
            state = States.Readjusting;
            
            var newPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0);
            
            while (true) {
                var moveDist = readjustSpeed * Time.fixedDeltaTime;
                var toNewPos = newPos - transform.position;
                
                if (toNewPos.magnitude <= moveDist) {
                    rb.MovePosition(newPos);
                    break;
                }
                rb.MovePosition(transform.position + toNewPos.normalized * moveDist);
                
                yield return new WaitForFixedUpdate();
            }

            state = States.Idle;
        }
    }
}
