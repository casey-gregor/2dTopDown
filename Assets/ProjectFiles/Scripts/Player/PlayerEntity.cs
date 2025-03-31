// Author: Egor Geisik

using System;
using ProjectFiles.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts.Player
{
    public sealed class PlayerEntity :  MonoBehaviour, IPlayer
    {
        public event Action OnPlayerDeath;
        public Transform PlayerTransform => transform;
        public bool IsDead => _isDead;
        public IHealthComponent HealthComponent => _healthComponent;
        
        private IHealthComponent _healthComponent;
        
        private bool _isDead;

        [Inject]
        private void Construct(
            IHealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
            
            _healthComponent.OnDeath += HandleDeathEvent;
        }

        private void HandleDeathEvent()
        {
            _isDead = true;
            OnPlayerDeath?.Invoke();
        }
        
        private void OnDestroy()
        {
            _healthComponent.OnDeath -= HandleDeathEvent;
        }
        
    }
}