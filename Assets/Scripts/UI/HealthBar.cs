using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Main.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _healthComponent;
        [SerializeField] private Image _fillImage;
        [SerializeField] private float _fadeOutTime; 
        [SerializeField] private float _fadeInTime; 

        private bool _parentIsUI;
        private float _timeSinceLastDamage;
        private bool _isVisible = false; 
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            if (_healthComponent != null)
            {
                _healthComponent.OnDamageTaken += HandleDamageTaken;
            }
        }

        private void OnDisable()
        {
            if (_healthComponent != null)
            {
                _healthComponent.OnDamageTaken -= HandleDamageTaken;
            }
        }

        private void Start()
        {
            _parentIsUI = transform.parent.GetComponent<RectTransform>() != null;

            if (_healthComponent != null && _fillImage != null)
            {
                _canvasGroup.alpha = 0;
                _isVisible = false;
            }
        }

        private void HandleDamageTaken(int healthChange)
        {
            _timeSinceLastDamage = 0;
            UpdateHealthBar(healthChange);
            ShowHealthBar(); 
        }

        private void UpdateHealthBar(int healthChange)
        {
            if (_fillImage != null)
            {
                SetFill(
                    (float)_healthComponent.CurrentHealth / _healthComponent.MaxHealth
                );
            }
        }

        private void SetFill(float value)
        {
            if (_fillImage != null)
            {
                _fillImage.fillAmount = Mathf.Clamp01(value);
            }
        }

        private void Update()
        {
            if (_parentIsUI) return;

            _timeSinceLastDamage += Time.deltaTime;

            if (_timeSinceLastDamage >= _fadeOutTime && _isVisible)
            {
                StartCoroutine(FadeOut());
            }
            else if (_timeSinceLastDamage < _fadeOutTime && !_isVisible)
            {
                StartCoroutine(FadeIn());
            }

            transform.localScale = new Vector3(
                transform.parent.localScale.x,
                transform.localScale.y,
                transform.localScale.z
            );
        }

        private void ShowHealthBar()
        {
            if (!_isVisible)
            {
                StopAllCoroutines();
                _canvasGroup.alpha = 1;
                _isVisible = true;
                _timeSinceLastDamage = 0;
            }
        }

        private IEnumerator FadeOut()
        {
            float elapsedTime = 0;
            while (elapsedTime < _fadeOutTime)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / _fadeOutTime);
                yield return null;
            }
            _canvasGroup.alpha = 0;
            _isVisible = false;
        }

        private IEnumerator FadeIn()
        {
            float elapsedTime = 0;
            while (elapsedTime < _fadeInTime)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / _fadeInTime);
                yield return null;
            }
            _canvasGroup.alpha = 1;
            _isVisible = true;
        }
    }
}
