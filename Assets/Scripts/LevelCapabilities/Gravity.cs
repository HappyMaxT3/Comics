using System.Collections;
using UnityEngine;

namespace Main
{
    public class SlowFallTrigger : MonoBehaviour
    {
        [SerializeField] private float slowFallMultiplier = 0.5f; // ��������� ���������� �������
        [SerializeField] private float slowFallDuration = 2f; // ������������ ���������� � ��������

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    // �������� ���������� �������� ����� ������� � ���������
                    Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>(), true);
                    StartCoroutine(SlowFall(playerRigidbody));
                }
            }
        }

        private IEnumerator SlowFall(Rigidbody2D playerRigidbody)
        {
            float originalGravityScale = playerRigidbody.gravityScale;
            playerRigidbody.gravityScale *= slowFallMultiplier;

            yield return new WaitForSeconds(slowFallDuration);

            playerRigidbody.gravityScale = originalGravityScale;

            // ���������� �������� ����� ������� � ���������
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerRigidbody.GetComponent<Collider2D>(), false);
        }
    }

}
