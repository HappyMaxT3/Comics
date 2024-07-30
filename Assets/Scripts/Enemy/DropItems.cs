using UnityEngine;

namespace Main
{
    public class DropItemOnDeath : MonoBehaviour
    {
        [SerializeField] private GameObject _itemPrefab; 
        [SerializeField] private Transform _projectileParent; 
        [SerializeField] private Transform _dropPoint; 
        [SerializeField] private float _dropForce = 10f; 
        private Health _health; 

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.OnDeath += HandleDeath;
            }
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.OnDeath -= HandleDeath; 
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
                    rb.velocity = Vector2.zero; 
                    Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized;
                    rb.AddForce(direction * _dropForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
