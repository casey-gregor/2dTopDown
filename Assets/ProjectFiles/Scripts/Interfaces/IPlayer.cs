using System;
using UnityEngine;

namespace ProjectFiles.Scripts.Interfaces
{
    public interface IPlayer
    {
        public event Action OnPlayerDeath;
        public Transform PlayerTransform { get; }
        public IHealthComponent HealthComponent { get; }
        public bool IsDead { get; }
    }
}