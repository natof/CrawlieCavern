using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace.objects.traps.objects {

    //TODO CLEAN
    /*public class SpikePress : Trap {
        private const float FallSpeed = 15f;
        private const float RiseSpeed = 5f;

        public LayerMask groundLayerMask;

        private GameManager _gameManager;
        private Vector2 _initialPosition;
        private bool _isFalling;
        private bool _isRising;

        private void Start() {
            _gameManager = GameObject.Find("GameController").GetComponent<GameManager>();
            StartCoroutine(Active());
            _initialPosition = transform.position;
        }

        private void Update() {

            if (!_gameManager.GameIsProgress()) {
                return;
            }
            if (_isFalling) {
                transform.Translate(Vector2.down * (FallSpeed * Time.deltaTime));
                return;
            }

            if (!_isRising) {
                return;
            }

            transform.position = Vector2.MoveTowards(transform.position, _initialPosition, RiseSpeed * Time.deltaTime);
            if (!(Vector2.Distance(transform.position, _initialPosition) < 0.01f)) {
                return;
            }
            _isRising = false;
            StartCoroutine(Active());
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if ((1 << collision.gameObject.layer & groundLayerMask) == 0) {
                return;
            }
            _isFalling = false;
            StartCoroutine(Rising());
        }

        private IEnumerator Active() {
            var random = new Random();
            yield return new WaitForSeconds(random.Next(4, 6));

            _isFalling = true;
        }

        private IEnumerator Rising() {
            yield return new WaitForSeconds(2);

            _isRising = true;
        }
    }*/
}