using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace {
    public class IntroductionCanva : Canva {

        public Text text;

        private Animator _animator;
        private AudioSource _audioSource;
        private string BaseText { get; set; }
        private GameManager GameManager { get; set; }

        private void Start() {
            GameManager = GameManager.Get();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();

            BaseText = text.text;
            text.text = "";

            GameManager.StopGame();
            StartCoroutine(WriteText());
        }

        private IEnumerator WriteText() {
            for (int i = 0; i <= BaseText.Length; i++) {
                text.text = BaseText[..i];
                _audioSource.PlayOneShot(_audioSource.clip);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(2f);

            for (int i = BaseText.Length; i >= 0; i--) {
                text.text = BaseText[..i];
                yield return new WaitForSeconds(0.01f);
            }

            GameManager.StartGame();
            _animator.SetBool("close", true);
            yield return new WaitForSeconds(1.30f);
            gameObject.SetActive(false);
        }
    }
}