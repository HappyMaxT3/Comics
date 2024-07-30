using Main.FSM;
using UnityEngine;

namespace Main
{
    public class EnemyChaseAndPatrol : MonoBehaviour
    {
        [Header("Chase")]
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private Vector2 _detectionRange;
        [SerializeField] private float _chaseSpeed;
        [SerializeField] private float _attackRange;

        private PatrolEnemy _patrolEnemy;
        private Transform _player;
        private bool _isChasing;

        private void Awake()
        {
            _patrolEnemy = GetComponent<PatrolEnemy>();
            if (_patrolEnemy == null)
            {
                Debug.LogError("PatrolEnemy component not found on the GameObject.");
            }
        }

        private void Update()
        {
            if (_isChasing)
            {
                ChasePlayer();
            }
            else
            {
                CheckForPlayer();
            }
        }

        private void CheckForPlayer()
        {
            RaycastHit2D hit = CheckPlayerHit(_detectionRange);
            if (hit)
            {
                _player = hit.collider.transform;
                if (_player != null)
                {
                    _isChasing = true;
                }
            }
        }

        private void ChasePlayer()
        {
            if (_player == null)
            {
                _isChasing = false;
                SwitchToPatrolState();
                return;
            }

            Vector2 targetPosition = new Vector2(_player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _chaseSpeed * Time.deltaTime);

            float distanceToPlayer = Vector2.Distance(new Vector2(transform.position.x, _player.position.y), _player.position);

            if (distanceToPlayer <= _attackRange)
            {

                _isChasing = false;
                SwitchToAttackState();
            }
            else if (!PlayerInDetectionRange())
            {

                _isChasing = false;
                SwitchToPatrolState();
            }
        }

        private bool PlayerInDetectionRange()
        {
            RaycastHit2D hit = CheckPlayerHit(_detectionRange);
            return hit.collider != null && hit.collider.GetComponent<IDamageable>() != null;
        }

        private void SwitchToAttackState()
        {
            Fsm fsm = GetFsm();
            if (fsm != null)
            {
                fsm.SetState<AttackState>();
            }
        }

        private void SwitchToPatrolState()
        {
            Fsm fsm = GetFsm();
            if (fsm != null)
            {
                fsm.SetState<PatrolState>();
            }
        }

        private Fsm GetFsm()
        {
            return (Fsm)_patrolEnemy.GetType().GetField("_fsm", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(_patrolEnemy);
        }

        private RaycastHit2D CheckPlayerHit(Vector2 range)
        {
            Vector2 forwardVector = (Vector2)_patrolEnemy.GetType().GetProperty("ForwardVector", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).GetValue(_patrolEnemy, null);
            Vector2 origin = new(
                transform.position.x + (forwardVector.x * range.x / 2),
                transform.position.y
            );
            RaycastHit2D hit = Physics2D.BoxCast(
                origin,
                range,
                0f,
                forwardVector,
                0,
                _playerLayerMask
            );
            return hit;
        }

        private void OnDrawGizmos()
        {
            if (_patrolEnemy != null)
            {
                Vector2 forwardVector = (Vector2)_patrolEnemy.GetType().GetProperty("ForwardVector", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public).GetValue(_patrolEnemy, null);
                Gizmos.color = Color.blue;
                Vector2 origin = new(
                    transform.position.x + (forwardVector.x * _detectionRange.x / 2),
                    transform.position.y
                );
                Gizmos.DrawWireCube(origin, _detectionRange);
            }
        }
    }
}
