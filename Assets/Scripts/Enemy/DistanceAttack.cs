using UnityEngine;

namespace Main
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

        private void Update()
        {
            DetectPlayer();
        }

        private void DetectPlayer()
        {
            bool playerDetected = false;

            // Проверяем игрока справа
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

            // Проверяем игрока слева
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

            if (playerDetected)
            {
                if (!_isPlayerDetected)
                {
                    BeginAttack();
                    _isPlayerDetected = true;
                }
            }
            else
            {
                if (_isPlayerDetected)
                {
                    EndAttack();
                    _isPlayerDetected = false;
                }
            }
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

            // Отразить анимацию в зависимости от направления стрельбы
            if (_player != null)
            {
                Vector3 directionToPlayer = (_player.position - transform.position).normalized;
                if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
                {
                    // Если направление стрельбы больше по X, отражаем спрайт
                    transform.localScale = new Vector3(Mathf.Sign(directionToPlayer.x), 1, 1);
                }
                else
                {
                    // Убедитесь, что враг не отображается по вертикали
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
                Vector3 toTarget = (_player.position - transform.position).normalized;
                toTarget.y = 0; // Стрелять только по оси X
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
