using UnityEngine;
using System.Collections;

namespace Main
{
    public class ShowUIOnTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject UIElement;
        [SerializeField] private Transform targetPosition;
        [SerializeField] private float duration = 3f;
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
            if (UIElement != null)
            {
                // Перемещение UI элемента к целевой позиции в игровом мире
                RectTransform rectTransform = UIElement.GetComponent<RectTransform>();
                Vector3 worldPosition = targetPosition.position;
                rectTransform.position = worldPosition;
                rectTransform.rotation = targetPosition.rotation;
                rectTransform.localScale = targetPosition.localScale; // Подстройка масштаба если нужно

                UIElement.SetActive(true);
                yield return FadeIn();

                yield return new WaitForSeconds(duration);

                yield return FadeOut();

                UIElement.SetActive(false);
            }
        }

        private IEnumerator FadeIn()
        {
            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Clamp01(elapsedTime);
                yield return null;
            }
        }

        private IEnumerator FadeOut()
        {
            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Clamp01(1 - elapsedTime);
                yield return null;
            }
        }
    }
}
