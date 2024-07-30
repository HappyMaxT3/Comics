using Main;
using UnityEngine;

namespace Main
{
    [RequireComponent(typeof(Collision2D))]
    public class InstantDamageDealer : MonoBehaviour
    {
        [SerializeField] private int _instantDamage;
        [SerializeField] private bool _knockbackApplied;
        [SerializeField] private float _knockbackForce;

        private void OnCollisionEnter2D(Collision2D other)
        {
            DealInstantDamage(other.gameObject.GetComponent<IDamageable>());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DealInstantDamage(other.GetComponent<IDamageable>());
        }

        public void SetDamageMultiplier(float multiplier)
        {
            _instantDamage = (int)(_instantDamage * multiplier);
        }

        private void DealInstantDamage(IDamageable damageable)
        {
            if (damageable == null) return;

            MonoBehaviour mb = (MonoBehaviour)damageable;
            if (_knockbackApplied)
            {
                Rigidbody2D rb;
                rb = mb.GetComponent<Rigidbody2D>();
                Vector2 knockbackDirection = Vector2.up;
                ApplyKnockback(rb, knockbackDirection, _knockbackForce);
            }
            damageable.TakeDamage(_instantDamage);
        }

        private void ApplyKnockback(Rigidbody2D rb, Vector2 direction, float knockForce)
        {
            //ForceMode2D.Impulse - мгновенное применение силы

            rb.AddForce(direction * knockForce, ForceMode2D.Impulse);
        }
    }
}