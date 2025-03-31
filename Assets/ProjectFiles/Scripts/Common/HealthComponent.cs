// Author: Egor Geisik

using System;
using ProjectFiles.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts
{
    public sealed class HealthComponent : IHealthComponent
    {
        public event Action OnDeath;
        public event Action<float> OnHealthChanged;
        public int MaxHealth => _maxHealth;
        
        private readonly int _maxHealth;
        private int _currentHealth;

        [Inject]
        public HealthComponent(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void RestoreHealth()
        {
            _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);
   
            if (_currentHealth == 0)
            {
                OnDeath?.Invoke();
            }
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }
}