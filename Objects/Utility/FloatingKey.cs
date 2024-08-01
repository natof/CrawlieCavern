using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace decor {
    public class FloatingKey : MonoBehaviour {
        
        //TODO CLEAN
        
        public GameObject player;
        public GameObject lockedDoor;
        
        private bool _isTake;
        private bool _keyOpenDoor;

        private const float MaxDistanceForTake = 1f;
        private const float MaxDistanceMove = 1.5f;

        private const float MaxDistanceDoor = 4f;

        private const float SmoothTime = 0.6f;
        private const float OscillationAmplitude = 0.1f;
        private const float OscillationFrequency = 2f;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _offset = Vector3.zero;

        private Light2D _light;
        private LockedDoor _lockedDoor;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        public Sprite keySide;

        private void Start() {
            _light = GetComponentInChildren<Light2D>();
            _lockedDoor = lockedDoor.GetComponent<LockedDoor>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Update() {
            if (_keyOpenDoor) {
                if (Vector2.Distance(_lockedDoor.GetPositionPadLock(), transform.position) <= 0.1f) {
                    StartCoroutine(StartOpenDoor());
                    return;
                }
            }
            
            if (_isTake) {
                if (PlayerIsCloseDoor(MaxDistanceDoor)) {
                    _keyOpenDoor = true;
                }
                return;
            }
            
            if (!PlayerIsClose(MaxDistanceForTake)) {
                return;
            }
            
            _isTake = true;
            StartCoroutine(PickupAnimation());
        }

        private void FixedUpdate() {
            if (!_isTake) {
                return;
            }
            
            Vector3? targetPosition = null;
            bool canOscilation = true;
            if (_keyOpenDoor) {
                targetPosition = _lockedDoor.GetPositionPadLock();

                Quaternion targetRotaion = Quaternion.Euler(0, 0, 90);
                if (Quaternion.Angle(transform.rotation, targetRotaion) > 0.1f) {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotaion, 90 * Time.fixedDeltaTime);
                }
                canOscilation = false;
            } else if (!PlayerIsClose(MaxDistanceMove)) {
                targetPosition = player.transform.position;
            }
            
            if (targetPosition == null) {
                return;
            }
            
            Vector3 position = transform.position;
            position = Vector3.SmoothDamp(position, (Vector3)targetPosition, ref _velocity, SmoothTime);

            if (canOscilation) {
                _offset.x = Mathf.Sin(Time.time * OscillationFrequency) * OscillationAmplitude;
                _offset.y = Mathf.Cos(Time.time * OscillationFrequency) * OscillationAmplitude;
                position += _offset;
            }
           
            transform.position = position;
        }

        private bool PlayerIsClose(float distanceCheck) {
            return Vector2.Distance(transform.position, player.transform.position) <= distanceCheck;
        }

        private bool PlayerIsCloseDoor(float distanceCheck) {
            return Vector2.Distance(lockedDoor.transform.position, player.transform.position) <= distanceCheck;
        }
        
        private IEnumerator PickupAnimation() {

            const float duration = 0.5f;
            float elapsed = 0f;

            float initialLight = _light.intensity;
            float targetIntensity = 2f;

            Vector3 initialScale = transform.localScale;
            Vector3 targetScale = initialScale * 2;
            
            while (elapsed < duration) {
                _light.intensity = Mathf.Lerp(initialLight, targetIntensity, elapsed / duration);
                transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
                transform.Rotate(new Vector3(0, 0, 360 * 2) * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0;
            while (elapsed < duration) {
                _light.intensity = Mathf.Lerp(targetIntensity, initialLight, elapsed / duration);
                transform.localScale = Vector3.Lerp(targetScale, initialScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }


        private IEnumerator StartOpenDoor() {
            _animator.enabled = false;
            _spriteRenderer.sprite = keySide;
            yield return new WaitForSeconds(1);
            _lockedDoor.Open();
            Destroy(gameObject);
        }
    }
}