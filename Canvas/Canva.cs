using UnityEngine;

namespace DefaultNamespace {
    public abstract class Canva : MonoBehaviour {
        public void SetActive(bool active = true) {
            gameObject.SetActive(active);
        }
    }
}