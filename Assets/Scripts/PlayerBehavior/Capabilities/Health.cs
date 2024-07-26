using System;
using UnityEngine;

namespace Main
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _defaultMaxHealth;
        public event Action OnDeath;
        public event Action<int> OnDamageTaken;

        private int _maxHealth;
        public int MaxHealth { get { return _maxHealth; } }

        private int _currentHealth;
        public int CurrentHealth { get { return _currentHealth; } }

        private float _hpMultiplier = 1f;
        private Animator _animator;
        private bool _isDead = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _maxHealth = _defaultMaxHealth;
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException($"Damage amount can't be negative!: {gameObject.name}");

            if (_isDead) return;

            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

            OnDamageTaken?.Invoke(amount);
            if (_currentHealth == 0)
            {
                OnDeath?.Invoke();
                if (_animator != null)
                {
                    _animator.SetBool("IsDead", true);
                }
                _isDead = true;
                DisableCharacterFunctionality();
            }
        }

        public void SetMaxHPMultiplier(float multiplier)
        {
            if (_isDead) return;

            _maxHealth = (int)(_defaultMaxHealth * multiplier);
            _currentHealth = _maxHealth;
            _hpMultiplier = multiplier;
        }

        private void DisableCharacterFunctionality()
        {
            var components = GetComponents<MonoBehaviour>();
            foreach (var component in components)
            {
                if (component != this)
                {
                    component.enabled = false;
                }
            }
        }
    }
}
