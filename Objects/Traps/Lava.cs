using System;
using UnityEngine;

namespace decor {
    public class Lava : MonoBehaviour {
        private Rigidbody2D _rigidbody2D;
        public bool _canMotion = true;
        private void Start() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Update()
        {
            if (!_canMotion) {
                _rigidbody2D.velocity = Vector2.zero;
                return;
            }
            _rigidbody2D.AddForce(new Vector2(0f, 0.05f), ForceMode2D.Force);
        }

        public void Stop() {
            _canMotion = false;
        }
    }
}