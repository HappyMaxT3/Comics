using UnityEngine;

namespace Main
{
    public class Gunfighter : MonoBehaviour
    {
        [SerializeField] public int maxHealth;
        private int currHealth;

        public Animator animator;

        private void Start()
        {
            currHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currHealth -= damage;

            animator.SetTrigger("Hurt");

            if(currHealth <= 0)
            {
                Die(); 
             }
        }

        private void Die()
        {
            Debug.Log("Enemy died");
            animator.SetBool("IsDead", true);

            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }

    }
}


