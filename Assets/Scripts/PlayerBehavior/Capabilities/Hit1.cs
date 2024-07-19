using UnityEngine;

namespace Main
{
    public class Hit1 : MonoBehaviour
    {
        private Animator animator;

        public Transform hitPoint;

        [SerializeField] public int attackDamage;
        [SerializeField] public float attackRange;
        [SerializeField] public float attackSpeed;

        public LayerMask enemyLayers;

        private float nextAttackTime = 0f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(Time.time > nextAttackTime)
            {
                if (Input.GetButtonDown("Attack"))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackSpeed;
                }

            }

        }

        private void Attack()
        {
            if (animator != null)
            {
                animator.SetTrigger("IsAttacking");
            }

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit to " + enemy.name);

                enemy.GetComponent<Gunfighter>().TakeDamage(attackDamage);
            }
        }



        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(hitPoint.position, attackRange);
        }
    }
}
