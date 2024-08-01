using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace DefaultNamespace {
    public class ButtonCanva : MonoBehaviour {
        [CanBeNull]
        private RectTransform RectTransform { get; set; }

        public void PointerEnter(GameObject button) {
            RectTransform = button.GetComponent<RectTransform>();
            StartCoroutine(AnimationHoverIn());
        }

        public void PointerExit() {
            StartCoroutine(AnimationHoverOut());
        }

        public void ButtonClickSound(AudioSource audioSource) {
            audioSource.Play();
        }

        private IEnumerator AnimationHoverIn() {
            if (RectTransform is null) {
                yield break;
            }
            for (float i = 0; i < 0.05; i += 0.01f) {
                yield return new WaitForSeconds(0.01f);
                RectTransform.localScale += new Vector3(i, i, i);
            }
        }

        private IEnumerator AnimationHoverOut() {
            if (RectTransform is null) {
                yield break;
            }
            for (float i = 0; i < 0.05; i += 0.01f) {
                yield return new WaitForSeconds(0.01f);
                RectTransform.localScale -= new Vector3(i, i, i);
            }
        }
    }
}