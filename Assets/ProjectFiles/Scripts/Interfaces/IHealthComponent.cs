using System;

namespace ProjectFiles.Scripts.Interfaces
{
    public interface IHealthComponent
    {
        public event Action OnDeath;
        public event Action<float> OnHealthChanged;
        
        public int MaxHealth { get; }
        public void TakeDamage(int damage);
        public void RestoreHealth();
    }
}