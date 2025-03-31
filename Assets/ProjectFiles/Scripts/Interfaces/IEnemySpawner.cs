using System;

namespace ProjectFiles.Scripts.Interfaces
{
    public interface IEnemySpawner
    {
        public event Action OnAllKilled;
    }
}