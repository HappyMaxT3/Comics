using UnityEngine;

namespace Main
{
    public class ParallaxBehavior : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField, Range(0, 1)] private float _horizontalMovementMultiper;
        [SerializeField, Range(0, 1)] private float _verticalMovementMultiper;

        private Vector3 _targetPosition => _target.position;

        private Vector3 _lastTargetPosition;

        private void Start()
        {
            _lastTargetPosition = _targetPosition;
        }

        private void Update()
        {
            Vector3 delta = _targetPosition - _lastTargetPosition;
            delta *= new Vector2(_horizontalMovementMultiper, _verticalMovementMultiper);
            transform.position += delta;
            _lastTargetPosition = _targetPosition;
        }
    }
}


