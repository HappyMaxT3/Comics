using UnityEngine;

namespace Main
{
    public class Rolling : MonoBehaviour
    {
        [SerializeField] private float rollingDuration = 0.5f;
        [SerializeField] private float rollingDistance = 2f;
        [SerializeField] private LayerMask enemyLayer; // Слой врагов

        private Vector2 rollingStart;
        private Vector2 rollingEnd;
        private float rollingTimeElapsed;
        private bool isRolling;

        private Animator animator;
        private Rigidbody2D rb;
        private Ground ground;
        private Collider2D playerCollider;

        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            ground = GetComponent<Ground>();
            playerCollider = GetComponent<Collider2D>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Rolling") && !isRolling && ground.OnGround)
            {
                StartRolling();
            }

            if (isRolling)
            {
                RollingToNewPosition();
            }
        }

        private void StartRolling()
        {
            isRolling = true;
            rb.velocity = Vector2.zero;
            if (!animator.GetBool("IsRolling"))
            {
                animator.SetBool("IsRolling", true);
            }

            // Игнорируем столкновения с врагами
            IgnoreCollisions(true);

            rollingStart = transform.position;
            rollingEnd = transform.localScale.x > 0 ? new Vector2(transform.position.x + rollingDistance, transform.position.y) : new Vector2(transform.position.x - rollingDistance, transform.position.y);
            rollingTimeElapsed = 0;
        }

        private void RollingToNewPosition()
        {
            rollingTimeElapsed += Time.deltaTime;
            float progress = rollingTimeElapsed / rollingDuration;

            float newX = Mathf.Lerp(rollingStart.x, rollingEnd.x, progress);
            transform.position = new Vector2(newX, transform.position.y);

            if (progress >= 1.0f)
            {
                StopRolling();
            }
        }

        private void StopRolling()
        {
            isRolling = false;
            animator.SetBool("IsRolling", false);

            // Восстанавливаем столкновения с врагами
            IgnoreCollisions(false);
        }

        private void IgnoreCollisions(bool ignore)
        {
            // Находим все коллайдеры, которые нужно игнорировать
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);
            foreach (Collider2D enemyCollider in enemyColliders)
            {
                if (ignore)
                {
                    Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);
                }
                else
                {
                    Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);
                }
            }
        }
    }
}
