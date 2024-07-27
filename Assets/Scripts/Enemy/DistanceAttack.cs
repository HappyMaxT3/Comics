using Main;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace BHSCamp
{
    public class RangedAttack : AttackBase
    {
        [SerializeField] private Projectile _projectilePrefab; // Префаб снаряда
        [SerializeField] private float _projectileSpawnOffset; // Отступ для спавна снаряда
        [SerializeField] private UnityEngine.Transform _projectileParent; // Родительский трансформ для снарядов
        [SerializeField] private LayerMask _playerLayerMask; // Маска слоя игрока
        [SerializeField] private Vector2 _detectionBoxSize; // Размер зоны обнаружения
        [SerializeField] private float _detectionDistance; // Расстояние для проверки
        private float _damageMultiplier = 1f;
        private UnityEngine.Transform _player; // Явное указание пространства имен
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

            // Повернуть врага в сторону игрока
            if (_player != null)
            {
                Vector3 directionToPlayer = (_player.position - transform.position).normalized;
                if (Mathf.Abs(directionToPlayer.x) > Mathf.Abs(directionToPlayer.y))
                {
                    // Отражаем спрайт в зависимости от направления
                    transform.localScale = new Vector3(Mathf.Sign(directionToPlayer.x), 1, 1);
                }
                else
                {
                    // Убедиться, что враг не отображается по вертикали
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }

        public override void EndAttack()
        {
            base.EndAttack();
            _animator.SetBool("IsShooting", false);

            // Создать снаряд, если игрок находится в зоне
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
