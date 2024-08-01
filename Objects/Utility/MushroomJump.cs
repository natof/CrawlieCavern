using UnityEngine;

namespace DefaultNamespace {
    public class MushroomJump : MonoBehaviour {
        private Animator Animator { get; set; }
        private const float JumpVelocity = 10;
        
        private void Start() {
            Animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (Player.TryGetPlayer(other.gameObject, out Player player)) {
                return;
            }
            player.PlayerMovement.Jump(JumpVelocity);
            Animator.SetTrigger("use"); 
        }
    }
}