using Main.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Main.UI
{
    public class LevelChooser : MonoBehaviour
    {
        [Header("Levels Data")]
        [SerializeField] private LevelPreviewData[] _levels;

        [Header("UI fields")]
        [SerializeField] private TMP_Text _descriptionText; 
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Image _lock;

        private int _currentLevelIndex = 0;

        private void OnEnable()
        {
            ShowLevel(_currentLevelIndex);
        }

        private void ShowLevel(int index)
        {
            LevelPreviewData level = _levels[index];
            _descriptionText.text = level.Description;
            _nameText.text = level.Name;
            _playButton.gameObject.SetActive(level.IsAccessible);
            _lock.enabled = !level.IsAccessible;
        }

        public void ShowPreviousLevel()
        {
            _currentLevelIndex = (_currentLevelIndex - 1 + _levels.Length) % _levels.Length;
            ShowLevel(_currentLevelIndex);
        }

        public void ShowNextLevel()
        {
            _currentLevelIndex = (_currentLevelIndex + 1) % _levels.Length;
            ShowLevel(_currentLevelIndex);
        }

        public void LoadChosenLevel()
        {
            SceneManager.LoadScene(_levels[_currentLevelIndex].SceneIndex);
            GameManager.Instance.SetLevelIndex(_currentLevelIndex);
        }
    }
}
