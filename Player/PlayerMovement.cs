using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace {
    [Serializable]
    public class PlayerMovement {
        private const float MoveSpeed = 250;
        private const float JumpForce = 5;
        private const float DashingVelocity = 14f;
        private const float DashingTime = 0.3f;
        private const int MaxDash = 1;
        private const float DashingCooldown = 1f;
        private const float GroundCheckRadius = 0.2f;
        private const float GhostEffectTime = 0.03f;

        public PlayerMovement(Player player) {
            Player = player;
            GroundCheck = Player.transform.Find("GroundCheck");
            CollisionLayers = LayerMask.GetMask("Default");

            GhostSpritePrefab = Resources.Load<GameObject>("GhostEffect");
            GhostRenderer = GhostSpritePrefab.GetComponent<SpriteRenderer>();
        }

        private float CurrentGhostTime { get; set; }

        private bool IsGrounded { get; set; }
        private bool IsJumping { get; set; }
        private bool IsDashing { get; set; }
        private bool CanDash { get; set; } = true;
        private int Dash { get; set; } = 1;
        private Vector2 DashingDirection { get; set; }
        private Vector3 Velocity { get; set; }
        private float HorizontalMovement { get; set; }
        private float VerticalMovement { get; set; }
        private Transform GroundCheck { get; set; }
        private LayerMask CollisionLayers { get; set; }

        private GameObject GhostSpritePrefab { get; set; }
        private SpriteRenderer GhostRenderer { get; set; }

        private Player Player { get; set; }

        private void Start() {
            //_ghostEffectComponent = GetComponent<PlayerGhostEffectComponent>();
        }

        public void HandleUpdate() {
            if (Player.IsFreeze) {
                return;
            }
            IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, CollisionLayers);

            if (IsGrounded) {
                Dash = MaxDash;
            }

            HorizontalMovement = Input.GetAxis("Horizontal") * MoveSpeed;

            if (Input.GetButtonDown("Jump") && IsGrounded) {
                IsJumping = true;
            }

            if (Input.GetKey(KeyCode.LeftShift) && CanDash && Dash > 0) {
                StartDash();
            }

            Rigidbody2D rb = Player.Rigidbody2D;

            Flip(rb.velocity.x);

            float characterVelocity = Mathf.Abs(rb.velocity.x);
            Player.Animator.SetFloat("Speed", characterVelocity);
        }

        public void HandleFixedUpdate() {
            if (Player.IsFreeze) {
                return;
            }
            MovePlayer(HorizontalMovement * Time.fixedDeltaTime);
        }

        private void MovePlayer(float horizontalMovement) {
            if (IsJumping) {
                Jump(JumpForce);
            }

            Rigidbody2D rb = Player.Rigidbody2D;
            Vector3 targetVelocity = new Vector2(horizontalMovement, rb.velocity.y);

            if (IsDashing) {
                HandleGhostEffect();
                if (DashingDirection.magnitude > 0.1f) {
                    DashingDirection.Normalize();
                }

                rb.velocity = new Vector2(
                    DashingDirection.x * DashingVelocity,
                    DashingDirection.y * DashingVelocity / 1.8f
                );
                return;
            }

            Vector3 currentVelocity = Velocity;
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, .05f);
        }

        public void Jump(float force) {
            Player.Rigidbody2D.AddForce(new Vector2(0f, force), ForceMode2D.Impulse);
            IsJumping = false;
        }

        private void StartDash() {
            DashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (DashingDirection == Vector2.zero) {
                return;
            }

            IsDashing = true;
            CanDash = false;
            Dash--;

            Player.Animator.SetBool("Dashing", true);
            Player.CameraShake.TriggerShake(0.1f, 0.05f);
            Player.Rigidbody2D.gravityScale = 0;

            Player.StartCoroutine(EndDash());
            Player.StartCoroutine(DashCooldown());
        }

        private IEnumerator DashCooldown() {
            yield return new WaitForSeconds(DashingCooldown);
            CanDash = true;
        }

        private IEnumerator EndDash() {
            yield return new WaitForSeconds(DashingTime);
            Player.Rigidbody2D.gravityScale = 1;
            Player.Animator.SetBool("Dashing", false);
            IsDashing = false;
        }

        private void Flip(float velocity) {
            SpriteRenderer spriteRenderer = Player.SpriteRenderer;
            spriteRenderer.flipX = velocity switch {
                > 0.1f => false,
                < -0.1f => true,
                _ => spriteRenderer.flipX
            };
        }

        private void HandleGhostEffect() {
            CurrentGhostTime += Time.deltaTime;

            if (!(CurrentGhostTime >= GhostEffectTime)) {
                return;
            }
            SpawnGhost();
            CurrentGhostTime = 0;
        }

        private void SpawnGhost() {
            GhostRenderer.sprite = Player.SpriteRenderer.sprite;
            GhostRenderer.flipX = Player.SpriteRenderer.flipX;
            GameObject ghost = Object.Instantiate(GhostSpritePrefab, Player.transform.position, Quaternion.identity);


            Object.Destroy(ghost, 0.4f);
        }
    }
}