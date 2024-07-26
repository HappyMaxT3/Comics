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
        [SerializeField] private AnimationClip _attackAnimationClip;

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
                animator.SetBool("IsAttacking", true);

                Invoke(nameof(EndAttack), GetAttackAnimationDuration());

            }



            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitPoint.position, attackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit to " + enemy.name);

                var Damagable = enemy.GetComponent<IDamageable>();

                if( Damagable != null )
                {
                    Damagable.TakeDamage(attackDamage);
                }

                
            }
        }

        private void EndAttack()
        {
            animator.SetBool("IsAttacking", false );
        }

        public float GetAttackAnimationDuration()
        {
            return _attackAnimationClip.length;
        }



        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(hitPoint.position, attackRange);
        }
    }
}
