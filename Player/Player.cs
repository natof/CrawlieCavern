using System.Diagnostics.CodeAnalysis;
using camera;
using UnityEngine;

namespace DefaultNamespace {
    public class Player : MonoBehaviour {
        public Animator Animator { get; set; }

        public Camera Camera { get; set; }

        //TODO REMOVE THIS
        public CameraShake CameraShake { get; set; }
        public Rigidbody2D Rigidbody2D { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }
        public PlayerMovement PlayerMovement { get; set; }
        public PlayerDataManager PlayerDataManager { get; set; }

        public bool IsDead { get; set; }
        public bool IsFreeze { get; set; }

        private void Awake() {
            PlayerMovement = new PlayerMovement(this);
            PlayerDataManager = new PlayerDataManager();

            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            Camera = Camera.main;
            if (Camera != null) {
                CameraShake = Camera.GetComponent<CameraShake>();
            }
        }

        private void Start() {
            PlayerDataManager.Load();
        }

        private void Update() {
            PlayerMovement.HandleUpdate();
        }

        private void FixedUpdate() {
            PlayerMovement.HandleFixedUpdate();
        }

        private void OnDisable() {
            PlayerDataManager.Save();
        }

        public void Kill(DeathCause deathCause = DeathCause.None) {
            Debug.Log("1");
            IsDead = true;
            EventManager.Trigger(new PlayerDeathEvent {
                Player = this,
                DeathCause = deathCause
            });
        }

        public void SetFreeze() {
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.gravityScale = 0f;
            IsFreeze = true;
        }

        public void UnsetFreeze() {
            Rigidbody2D.gravityScale = 1f;
            IsFreeze = false;
        }

        public static bool TryGetPlayer(GameObject gameObject, [NotNullWhen(true)] out Player player) {
            if (gameObject.TryGetComponent(out player) && gameObject.CompareTag("Player")) {
                return true;
            }
            player = null;
            Debug.Log("NULL");
            return false;
        }

        public static Player GetPlayer() {
            return GameObject.Find("Player").GetComponent<Player>();
        }
    }
}