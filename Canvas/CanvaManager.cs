using UnityEngine;

namespace DefaultNamespace {
    public class CanvaManager : MonoBehaviour {

        public GameObject deathCanvaObject;
        public GameObject hudCanvaObject;
        public DeathCanva DeathCanva { get; private set; }
        public HudCanva HudCanva { get; private set; }

        private void Awake() {
            DeathCanva = deathCanvaObject.GetComponent<DeathCanva>();
            HudCanva = hudCanvaObject.GetComponent<HudCanva>();
        }


        public static CanvaManager Get() {
            return GameObject.Find("Canvas").GetComponent<CanvaManager>();
        }
    }
}