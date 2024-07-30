using Main;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace BHSCamp
{
    public class RangedAttack : AttackBase
    {
        [SerializeField] private Projectile _projectilePrefab; 
        [SerializeField] private float _projectileSpawnOffset; 
        [SerializeField] private UnityEngine.Transform _projectileParent;
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private Vector2 _detectionBoxSize; 
        [SerializeField] private float _detectionDistance; 
        private float _damageMultiplier = 1f;
        private UnityEngine.Transform _player;
        private bool _isPlayerDetected = false;
        private bool _isAttacking = false;

        private void Update()
        {
            DetectPlayer();
            if (_isPlayerDetected)
            {
                if (!_isAttacking)
                {
                    BeginAttack();
                    _isAttacking = true;
                }
            }
            else
            {
                if (_isAttacking)
                {
                    EndAttack();
                    _isAttacking = false;
                }
            }
        }

        private void DetectPlayer()
        {
            bool playerDetected = false;

            Vector2 originRight = new Vector2(transform.position.x + _detectionDistance, transform.position.y);
            RaycastHit2D hitRight = Physics2D.BoxCast(
                originRight,
                _detectionBoxSize,
                0f,
                Vector2.right,
                0,
                _playerLayerMask
            );

            if (hitRight.collider != null && hitRight.collider.CompareTag("Player"))
            {
                _player = hitRight.transform;
                playerDetected = true;
            }

            Vector2 originLeft = new Vector2(transform.position.x - _detectionDistance, transform.position.y);
            RaycastHit2D hitLeft = Physics2D.BoxCast(
                originLeft,
                _detectionBoxSize,
                0f,
                Vector2.left,
                0,
                _playerLayerMask
            );

            if (hitLeft.collider != null && hitLeft.collider.CompareTag("Player"))
            {
                _player = hitLeft.transform;
                playerDetected = true;
            }

            _isPlayerDetected = playerDetected;
        }

        public override void SetDamageMultiplier(float multiplier)
        {
            _damageMultiplier = multiplier;
        }

        public override void BeginAttack()
        {
            if (IsAttacking) return;
            base.BeginAttack();
            _animator.SetBool("IsShooting", true);

            if (_player != null)
            {
                Vector3 directionToPlayer = (_player.position - transform.position).normalized;
                if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
                {
                    transform.localScale = new Vector3(Mathf.Sign(directionToPlayer.x), 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }

        public override void EndAttack()
        {
            base.EndAttack();
            _animator.SetBool("IsShooting", false);

            if (_player != null)
            {
                Debug.Log(_player);
                Vector3 toTarget = (_player.position - transform.position).normalized;
                toTarget.y = 0; 
                Projectile projectile = Instantiate(
                _projectilePrefab,
                    transform.position + toTarget * _projectileSpawnOffset,
                    Quaternion.identity,
                    _projectileParent
                );
                projectile.SetDirection(toTarget);
                projectile.SetDamageMultiplier(_damageMultiplier);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(
            new Vector2(transform.position.x + _detectionDistance, transform.position.y),
                _detectionBoxSize
            );
            Gizmos.DrawWireCube(
            new Vector2(transform.position.x - _detectionDistance, transform.position.y),
                _detectionBoxSize
            );
        }
    }
}
