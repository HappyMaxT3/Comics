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
        public LayerMask destructibleLayers;

        private float nextAttackTime = 0f;
        private bool isAttacking = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Time.time > nextAttackTime)
            {
                if (Input.GetButtonDown("Attack"))
                {
                    isAttacking = true;
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

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit to " + enemy.name);

                var Damagable = enemy.GetComponent<IDamageable>();

                if (Damagable != null)
                {
                    Damagable.TakeDamage(attackDamage);
                }
            }

            Collider2D[] hitDestructibles = Physics2D.OverlapCircleAll(hitPoint.position, attackRange, destructibleLayers);
            foreach (Collider2D destructible in hitDestructibles)
            {
                Debug.Log("Hit destructible " + destructible.name);

                var destroyable = destructible.GetComponent<Destroying>();
                if (destroyable != null)
                {
                    destroyable.ExplodeTheObject(); 
                }
            }

            isAttacking = false; 
        }

        private void EndAttack()
        {
            animator.SetBool("IsAttacking", false);
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
