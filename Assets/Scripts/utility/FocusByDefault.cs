using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace utility {
    public class FocusByDefault : MonoBehaviour
    {
        private void OnEnable() {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}