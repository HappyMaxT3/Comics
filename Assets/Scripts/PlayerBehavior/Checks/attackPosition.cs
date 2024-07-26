using UnityEngine;

namespace Main
{
    public class AttackPosition : MonoBehaviour
    {
        private Vector3 attackDirection;

        private void Update()
        {
            if (Input.GetButtonDown("Attack"))
            {
                DetermineAttackDirection();
            }
        }

        private void DetermineAttackDirection()
        {
            attackDirection = transform.localScale.x > 0 ? Vector3.right : Vector3.left; // Определяем направление атаки
        }

        public Vector3 GetAttackDirection()
        {
            return attackDirection;
        }
    }
}
