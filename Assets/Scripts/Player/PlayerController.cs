using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

namespace Player {
    public class PlayerController : MonoBehaviour {
        private static readonly int OnReady = Animator.StringToHash("OnReady");
        private static readonly int OnJump = Animator.StringToHash("OnJump");
        private static readonly int OnLand = Animator.StringToHash("OnLand");
        private static readonly int OnDeath = Animator.StringToHash("OnDeath");
        
        [SerializeField] private AudioResource jumpSfx;
        
        private Rigidbody2D rb;
        private GameManager gameManager;
        private Animator animator;
        private SpriteRenderer sprite;

        private bool dead = false;
        
        [SerializeField] private float moveSecs;
        [SerializeField] private float readjustSpeed;
        public enum States {MovementDisabled, Idle, Ready, Moving, Readjusting}
        private Coroutine currentRoutine;

        public States State { get; private set; } = States.Idle;

        public bool CanReady() => State == States.Idle;
        public bool CanMove() => State == States.Ready;

        
        private void Start() {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sprite = GetComponentsInChildren<SpriteRenderer>()[0];
            gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        }

        private CardinalDirection readiedDirection;
        public void Ready(CardinalDirection direction) {
            State = States.Ready;
            readiedDirection = direction;
            
            if(readiedDirection == CardinalDirection.Right) sprite.flipX = true;
            if(readiedDirection == CardinalDirection.Left) sprite.flipX = false;
            if(!dead) animator.SetTrigger(OnReady);
        }

        public void Move() {
            currentRoutine = StartCoroutine(Move(readiedDirection));
        }

        public void DisableMovement() {
            State = States.MovementDisabled;
            StopAllCoroutines();
        }
        
        private Vector3 startPos;
        private IEnumerator Move(CardinalDirection direction) {
            State = States.Moving;
            if(!dead) animator.SetTrigger(OnJump);
            
            float elapsedTime = 0;

            startPos = transform.position;
            var endPos = transform.position + direction.ToVector3();
            AudioManager.PlaySfx(jumpSfx, AudioManager.SfxGroup);

            while (elapsedTime < moveSecs) {
                elapsedTime += Time.fixedDeltaTime;
                rb.MovePosition(Vector3.Lerp(startPos, endPos, Mathf.Min(1, elapsedTime / moveSecs)));
                yield return new WaitForFixedUpdate();
            }
            StartCoroutine(Readjust(endPos));
        }
        
        private IEnumerator Readjust(Vector3 pos) {
            State = States.Readjusting;
            if(!dead) animator.SetTrigger(OnLand);
            
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

            State = States.Idle;
        }
        
        
        public void OnCollisionEnter2D(Collision2D other) {
            if(State == States.Readjusting) return;
            
            StopCoroutine(currentRoutine);
            StartCoroutine(Readjust(startPos));
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.layer != LayerMask.NameToLayer("DeathZone")) return;
            dead = true;
            
            animator.SetTrigger(OnDeath);

            gameManager.Death();
        }
    }
}
