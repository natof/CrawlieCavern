using System;
using Unity.VisualScripting;
using UnityEngine;

namespace decor {
    public class LockedDoor : MonoBehaviour {
        //TODO CLEAN
        public Sprite doorOpenSprite;
        public GameObject doorLayer;
        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer _spriteRendererLayer;
        private BoxCollider2D _boxCollider;
        public GameObject padLock;
        
        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRendererLayer = doorLayer.GetComponent<SpriteRenderer>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        public void Open() {
            _spriteRenderer.sprite = doorOpenSprite;
            _boxCollider.enabled = false;
            _spriteRendererLayer.enabled = false;
        }

        public Vector3 GetPositionPadLock() {
            return padLock.transform.position;
        }
    }
}