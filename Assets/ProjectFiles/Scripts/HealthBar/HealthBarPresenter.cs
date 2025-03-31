// Author: Egor Geisik

using System;
using ProjectFiles.Scripts.Interfaces;
using Zenject;

namespace ProjectFiles.Scripts.HealthBar
{
    public sealed class HealthBarPresenter : IDisposable
    {
        private IHealthComponent _healthComponent;
        private IView<float> _sliderView;

        [Inject]
        public HealthBarPresenter(
            IHealthComponent healthComponent, 
            IView<float> sliderView)
        {
            _healthComponent = healthComponent;
            _sliderView = sliderView;

            _sliderView.SetupView(_healthComponent.MaxHealth);
            _healthComponent.OnHealthChanged += HandleHealthChanged;
        }

        public void Dispose()
        {
            _healthComponent.OnHealthChanged -= HandleHealthChanged;
        }
        
        private void HandleHealthChanged(float value)
        {
            _sliderView.UpdateData(value);
        }
        
    }
}