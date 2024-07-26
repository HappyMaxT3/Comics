using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevelTrigger : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private int currentLevelIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Main.SaveLoadSystem.SaveLevel(currentLevelIndex);

            SceneManager.LoadScene(nextSceneName);
        }
    }
}
