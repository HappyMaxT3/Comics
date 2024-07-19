using System;
using UnityEngine;

namespace Main
{
    //���������, ���������� �� �������� ��������
    public class Health : MonoBehaviour, IDamageable, IHealable
    {
        public event Action OnDeath; //�������, ���������� ��� ������
        public event Action<int> OnDamageTaken; //�������, ���������� ��� ��������� �����
        public event Action<int> OnHealed; //�������, ���������� ��� �������������� ��������

        [SerializeField] private int _maxHealth = 100;

        private int _currentHealth;

        public int CurrentHealth { get { return _currentHealth; } }
        public int MaxHealth { get { return _maxHealth; } }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (amount < 0) //������ ������� ������������� ����
                throw new ArgumentOutOfRangeException($"Damage amount can't be negative!: {gameObject.name}");

            _currentHealth -= amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth); //�������� �� ����� ���� < 0

            if (_currentHealth == 0)
            {
                OnDeath?.Invoke();
            }
            else
            {
                OnDamageTaken?.Invoke(amount);
            }
        }

        public void Heal(int amount)
        {
            if (amount < 0) // ������ �������� ������������� ���-�� ��
                throw new ArgumentOutOfRangeException($"amount should be positive: {gameObject.name}");
            _currentHealth += amount;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            OnHealed?.Invoke(amount);
        }
    }
}