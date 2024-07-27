using Main;
using UnityEngine;

namespace Main
{
    public class MeleeAttack : AttackBase
    {
        [SerializeField] InstantDamageDealer _damageDealer;

        public override void SetDamageMultiplier(float multiplier)
        {
            _damageDealer.SetDamageMultiplier(multiplier);
        }

        public override void BeginAttack()
        {
            if (IsAttacking) return;

            base.BeginAttack();
            _animator.SetBool("IsAttacking", true);
        }

        public override void EndAttack()
        {
            base.EndAttack();
            _animator.SetBool("IsAttacking", false);
        }
    }
}