using UnityEngine;

namespace DefaultNamespace {
    public class Lava : DeathCollisionTrap {
        private Rigidbody2D Rb { get; set; }
        private GameManager GameManager { get; set; }
        
        private const float Speed = 0.05f;
        
        private void Awake() {
            Rb = GetComponent<Rigidbody2D>();
            GameManager = GameManager.Get();
        }

        public void Update() 
        {
            if (!GameManager.GameIsInProgress) {
                Rb.velocity = Vector2.zero;
                return;
            }
            Rb.AddForce(new Vector2(0f, Speed), ForceMode2D.Force);
        }

        protected override DeathCause GetPlayerDeathCause() {
            return DeathCause.Fire;
        }
    }
}