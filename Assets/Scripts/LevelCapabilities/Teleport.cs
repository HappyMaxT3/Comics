using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Main
{
    public class Teleport : MonoBehaviour
    {
        public GameObject portal;
        public Image fadeScreen; 
        public float fadeDuration;
        public List<GameObject> enemies;

        private GameObject player;
        private Rigidbody2D playerRigidbody;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            playerRigidbody = player.GetComponent<Rigidbody2D>();

            if (fadeScreen != null)
            {
                Color color = fadeScreen.color;
                color.a = 0f;
                fadeScreen.color = color;
            }
            else
            {
                Debug.LogError("FadeScreen Image is not assigned in the inspector.");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (AreAllEnemiesDead())
                {
                    StartCoroutine(TeleportAndFade());
                }
                else
                {
                    Debug.Log("Not all enemies are dead.");
                }
            }
        }

        private IEnumerator TeleportAndFade()
        {
            if (fadeScreen != null)
            {
                // Затемнение экрана
                yield return StartCoroutine(FadeScreen(1f));
            }

            Vector3 teleportPosition = portal.transform.position;

            if (playerRigidbody.velocity.x > 0)
            {
                teleportPosition.x += 1;
            }
            else if (playerRigidbody.velocity.x < 0)
            {
                teleportPosition.x -= 1;
            }

            player.transform.position = teleportPosition;

            if (fadeScreen != null)
            {
                yield return StartCoroutine(FadeScreen(0f));
            }
        }

        private IEnumerator FadeScreen(float targetAlpha)
        {
            Color startColor = fadeScreen.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                fadeScreen.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
                yield return null;
            }

            fadeScreen.color = endColor;
        }

        private bool AreAllEnemiesDead()
        {
            foreach (var enemy in enemies)
            {
                var health = enemy.GetComponent<Health>();
                if (health != null && health.CurrentHealth > 0)
                {
                    return false; 
                }
            }
            return true; 
        }
    }
}
