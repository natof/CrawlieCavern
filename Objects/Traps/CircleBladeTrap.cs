using UnityEngine;

namespace DefaultNamespace {
    public class CircleBladeTrap : DeathCollisionTrap {

        private const float Speed = 7f;
        public GameObject targetPositionObject;

        private bool _hasReachTargetPosition;

        private Vector2 _initialPosition;
        private Vector2 _targetPosition;

        private void Start() {
            _initialPosition = transform.position;
            _targetPosition = targetPositionObject.transform.position;
        }

        private void Update() {
            Vector2 motionPosition = _targetPosition;
            if (_hasReachTargetPosition) {
                motionPosition = _initialPosition;
            }

            transform.position = Vector2.MoveTowards(transform.position, motionPosition, Speed * Time.deltaTime);
            if (!(Vector2.Distance(transform.position, motionPosition) < 0.01f)) {
                return;
            }

            _hasReachTargetPosition = !_hasReachTargetPosition;
        }

        protected override DeathCause GetPlayerDeathCause() {
            return DeathCause.CicleBlade;
        }
    }
}