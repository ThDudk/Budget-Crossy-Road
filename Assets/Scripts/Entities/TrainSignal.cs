using UnityEngine;

namespace Entities {
    public class TrainSignal : MonoBehaviour {
        [SerializeField] private float xVariation = 0;
        private static readonly int Flashing = Animator.StringToHash("Flashing");
        private Animator animator;

        private void Start() {
            animator = GetComponent<Animator>();
            transform.localPosition += Vector3.right * xVariation * Random.Range(-1f, 1f);
        }

        private void Update() {
            animator.SetBool(Flashing, TrainInThisLane());
        }

        private bool TrainInThisLane() {
            for (var i = 0; i < transform.parent.childCount; i++) {
                var child = transform.parent.GetChild(i);
                if (child.TryGetComponent<Train>(out _)) {
                    return true;
                }
            }
            return false;
        }
    }
}
