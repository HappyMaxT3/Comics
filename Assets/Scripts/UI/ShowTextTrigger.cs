using System.Collections;
using UnityEngine;

namespace Main
{
    public class ShowTextTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject UIElement;
        [SerializeField] private float duration; 
        [SerializeField] private float fadeDuration; 

        private CanvasGroup canvasGroup;

        private void Start()
        {
            if (UIElement != null)
            {
                canvasGroup = UIElement.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = UIElement.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0;
                UIElement.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(ShowUIElement());
            }
        }

        private IEnumerator ShowUIElement()
        {
            UIElement.SetActive(true);
            yield return FadeIn();

            yield return new WaitForSeconds(duration);

            yield return FadeOut();

            UIElement.SetActive(false);
        }

        private IEnumerator FadeIn()
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                yield return null;
            }
            canvasGroup.alpha = 1f;
        }

        private IEnumerator FadeOut()
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }
    }
}
