using UnityEngine;
using UnityEngine.UI; // ����������� ��� ������ � UI
using System.Collections;

namespace Main
{
    public class Teleport : MonoBehaviour
    {
        public GameObject portal;
        public Image screenFader; // ������ �� UI-������� ��� ���������� ������
        public float fadeDuration = 0.5f; // ������������ ������� ����������

        private GameObject player;
        private Rigidbody2D playerRigidbody;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            playerRigidbody = player.GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(TeleportAndFade());
            }
        }

        private IEnumerator TeleportAndFade()
        {
            // �������� �������� ����������
            yield return StartCoroutine(FadeScreen(1f)); // ��������� �����

            Vector3 teleportPosition = portal.transform.position;
            Vector3 prevPosition = player.transform.position;

            if (playerRigidbody.velocity.x > 0)
            {
                teleportPosition.x += 1;
            }
            else if (playerRigidbody.velocity.x < 0)
            {
                teleportPosition.x -= 1;
            }

            player.transform.position = teleportPosition;

            yield return StartCoroutine(FadeScreen(0f));
        }

        private IEnumerator FadeScreen(float targetAlpha)
        {
            Color startColor = screenFader.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                screenFader.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
                yield return null;
            }

            screenFader.color = endColor;
        }
    }
}
