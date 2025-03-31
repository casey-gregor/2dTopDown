using UnityEngine;
using UnityEngine.UI;

namespace ProjectFiles.Scripts.HealthBar
{
    public sealed class HealthBarView : MonoBehaviour, IView<float>
    {
        [SerializeField] private Slider slider;
        [SerializeField] private float visibilityTimer;
        [SerializeField] private Transform target;

        private Image[] _imageComponents;
        private Color _tempFillColor;
        
        private float _timer;
        private bool _sliderVisible;

        public void SetupView(float value)
        {
            slider.maxValue = value;
            slider.value = value;
        }
        
        public void UpdateData(float value)
        {
            ChangeSliderValue(value);
        }
        
        private void Awake()
        {
            if (slider == null)
            {
                Debug.LogError($"{nameof(HealthBarView)}.{nameof(slider)} is null");
            }
            SetSliderVisibility(0);
        }

        private void OnEnable()
        {
            _imageComponents = GetComponentsInChildren<Image>();
        }

        private void Update()
        {
            AutoHideSlider();
        }
        
        private void ChangeSliderValue(float amount)
        {
            slider.value = Mathf.Max(0,  amount);
            SetSliderVisibility(1);
            _timer = visibilityTimer;
        }

        private void SetSliderVisibility(float value)
        {
            if (_imageComponents is { Length: > 0 })
            {
                foreach (var imageComponent in _imageComponents)
                {
                    _tempFillColor = imageComponent.color;
                    _tempFillColor.a = value;
                    imageComponent.color = _tempFillColor;
                }
            }

            _sliderVisible = value > 0;
        }

        private void AutoHideSlider()
        {
            if (_sliderVisible)
            {
                _timer -= Time.deltaTime;
            }
            if (_timer <= 0)
            {
                SetSliderVisibility(0);
            }
        }
    }
}