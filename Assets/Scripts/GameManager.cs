using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Main.UI;

namespace Main
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private LevelPreviewData[] _levels;
        private int _currentLevelIndex;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void FinishCurrentLevel()
        {
            SceneManager.LoadScene("MainMenu"); 
            OpenAccessToNextlevel();
        }

        private void OpenAccessToNextlevel()
        {
            if (_currentLevelIndex + 1 == _levels.Length)
                return;

            _levels[_currentLevelIndex + 1].IsAccessible = true;
        }

        public void SetLevelIndex(int newIndex)
        {
            _currentLevelIndex = newIndex;
        }
    }
}