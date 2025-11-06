using UnityEngine;
using UnityEngine.Audio;

namespace Entities {
    public class TrainSignal : MonoBehaviour {
        [SerializeField] private float xVariation = 0;
        private static readonly int Flashing = Animator.StringToHash("Flashing");
        private Animator animator;
        [SerializeField] private AudioResource sound;

        private bool wasPreviouslyFlashing;

        private void Start() {
            animator = GetComponent<Animator>(); ;
            transform.localPosition += Vector3.right * xVariation * Random.Range(-1f, 1f);
        }

        private void Update() {
            animator.SetBool(Flashing, TrainInThisLane());

            if (TrainInThisLane() && !wasPreviouslyFlashing) {
                wasPreviouslyFlashing = true;
                Audio.PlaySfx(sound, Audio.SfxGroup);
            }

            if (!TrainInThisLane()) wasPreviouslyFlashing = false;
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
