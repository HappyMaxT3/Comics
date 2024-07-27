using UnityEngine;

namespace Main
{
    public class DropItemOnDeath : MonoBehaviour
    {
        [SerializeField] private GameObject _itemPrefab; // Префаб предмета для выпадения
        [SerializeField] private Transform _projectileParent; // Родительский объект для предмета
        [SerializeField] private Transform _dropPoint; // Точка для появления предмета
        [SerializeField] private float _dropForce = 10f; // Сила выброса предмета

        private Health _health; // Ссылка на скрипт Health

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.OnDeath += HandleDeath; // Подписываемся на событие смерти
            }
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.OnDeath -= HandleDeath; // Отписываемся от события смерти
            }
        }

        private void HandleDeath()
        {
            DropItem();
        }

        private void DropItem()
        {
            if (_itemPrefab != null && _dropPoint != null)
            {
                GameObject item = Instantiate(_itemPrefab, _dropPoint.position, Quaternion.identity, _projectileParent);
                Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero; // Обеспечиваем нулевую скорость перед применением силы
                    Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized; // Случайное направление
                    rb.AddForce(direction * _dropForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
