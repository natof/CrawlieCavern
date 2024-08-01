using UnityEngine;

namespace camera {
    public class CameraShake : MonoBehaviour {
        public Transform cameraTransform;
        public float shakeDuration = 0.5f;
        public float shakeMagnitude = 0.2f;
        private float _currentShakeDuration;

        private Vector3 _originalPosition;

        private void Start() {
            if (cameraTransform == null) {
                cameraTransform = GetComponent<Transform>();
            }
            _originalPosition = cameraTransform.localPosition;
        }

        public void Update() {
            if (_currentShakeDuration > 0) {
                cameraTransform.localPosition = _originalPosition + Random.insideUnitSphere * shakeMagnitude;
                _currentShakeDuration -= Time.deltaTime;
            } else {
                cameraTransform.localPosition = _originalPosition;
            }
        }

        public void TriggerShake(float duration, float magnitude) {
            shakeDuration = duration;
            shakeMagnitude = magnitude;
            _currentShakeDuration = duration;
        }
    }
}