using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class DeathScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject deathScreen;
        [SerializeField] private float deathScreenDuration; 

        public void ShowDeathScreen()
        {
            if (deathScreen != null)
            {
                deathScreen.SetActive(true); 
                Invoke("RestartScene", deathScreenDuration); 
            }
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
