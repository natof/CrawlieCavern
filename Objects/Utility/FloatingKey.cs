using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace decor {
    public class FloatingKey : MonoBehaviour {
        public GameObject lockedDoor;
        public Sprite keySide;
        
        private LockedDoor LockedDoor { get; set; }
        private SpriteRenderer SpriteRenderer { get; set; }
        private Animator Animator { get; set; }
        private Player Player { get; set; }
        private bool IsTake { get; set; }
        private bool KeyOpenDoor { get; set; }

        private const float MaxDistanceForTake = 1f;
        private const float MaxDistanceMove = 1.5f;
        private const float MaxDistanceDoor = 4f;
        private const float SmoothTime = 0.6f;
        private const float OscillationAmplitude = 0.1f;
        private const float OscillationFrequency = 2f;

        private Vector3 _velocity = Vector3.zero;
        private Vector3 _offset = Vector3.zero;

        private Light2D Light { get; set; }
        
        private void Start() {
            Light = GetComponentInChildren<Light2D>();
            LockedDoor = lockedDoor.GetComponent<LockedDoor>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
        }

        private void Update() {
            if (KeyOpenDoor) {
                if (Vector2.Distance(LockedDoor.GetPositionPadLock(), transform.position) <= 0.1f) {
                    StartCoroutine(StartOpenDoor());
                    return;
                }
            }
            
            if (IsTake) {
                if (PlayerIsCloseDoor(MaxDistanceDoor)) {
                    KeyOpenDoor = true;
                }
                return;
            }
            
            if (!PlayerIsClose(MaxDistanceForTake)) {
                return;
            }
            
            IsTake = true;
            StartCoroutine(PickupAnimation());
        }

        private void FixedUpdate() {
            if (!IsTake) {
                return;
            }
            
            Vector3? targetPosition = null;
            bool canOscilation = true;
            if (KeyOpenDoor) {
                targetPosition = LockedDoor.GetPositionPadLock();

                Quaternion targetRotaion = Quaternion.Euler(0, 0, 90);
                if (Quaternion.Angle(transform.rotation, targetRotaion) > 0.1f) {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotaion, 90 * Time.fixedDeltaTime);
                }
                canOscilation = false;
            } else if (!PlayerIsClose(MaxDistanceMove)) {
                targetPosition = Player.transform.position;
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
            return Vector2.Distance(transform.position, Player.transform.position) <= distanceCheck;
        }

        private bool PlayerIsCloseDoor(float distanceCheck) {
            return Vector2.Distance(lockedDoor.transform.position, Player.transform.position) <= distanceCheck;
        }
        
        private IEnumerator PickupAnimation() {

            const float duration = 0.5f;
            float elapsed = 0f;

            float initialLight = Light.intensity;
            float targetIntensity = 2f;

            Vector3 initialScale = transform.localScale;
            Vector3 targetScale = initialScale * 2;
            
            while (elapsed < duration) {
                Light.intensity = Mathf.Lerp(initialLight, targetIntensity, elapsed / duration);
                transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
                transform.Rotate(new Vector3(0, 0, 360 * 2) * Time.deltaTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0;
            while (elapsed < duration) {
                Light.intensity = Mathf.Lerp(targetIntensity, initialLight, elapsed / duration);
                transform.localScale = Vector3.Lerp(targetScale, initialScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }


        private IEnumerator StartOpenDoor() {
            Animator.enabled = false;
            SpriteRenderer.sprite = keySide;
            yield return new WaitForSeconds(1);
            LockedDoor.Open();
            Destroy(gameObject);
        }
    }
}