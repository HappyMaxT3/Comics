using Main;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevelTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.FinishCurrentLevel();
        }
    }
}
