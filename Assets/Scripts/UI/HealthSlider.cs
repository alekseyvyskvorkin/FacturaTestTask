using TestTask.Abstractions;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
    [System.Serializable]
    public class HealthSlider
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Slider _slider;

        private BaseUnit _baseUnit;
        private Transform _lookTarget;

        public void Initialize(BaseUnit baseUnit, Transform followCamera)
        {
            _baseUnit = baseUnit;
            _baseUnit.OnTakeDamage += UpdateSlider;
            _baseUnit.OnDie += DeactivateSlider;
            _baseUnit.OnSpawn += ActivateSlider;

            _slider.maxValue = _baseUnit.MaxHealth;
            _slider.value = _baseUnit.MaxHealth;

            _lookTarget = followCamera;
        }

        public void LookAtCamera()
        {
            _canvas.transform.LookAt(_lookTarget);
        }

        private void DeactivateSlider()
        {
            _canvas.gameObject.SetActive(false);
        }

        private void ActivateSlider()
        {
            _canvas.gameObject.SetActive(true);
            _slider.value = _baseUnit.MaxHealth;
        }

        private void UpdateSlider(int damage)
        {
            _slider.value = _baseUnit.Health;
        }
    }
}