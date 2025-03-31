// Author: Egor Geisik

using ProjectFiles.Scripts.Interfaces;

namespace ProjectFiles.Scripts
{
    public sealed class IntervalSpawnStrategy :  ISpawnStrategy
    {
        public bool ShouldSpawn(float timer, int remainingEnemies, bool gameOver)
        {
            return timer <= 0 && remainingEnemies > 0 && !gameOver;
        }

        public float GetNextTimer(float currentTimer, float interval, float deltaTime)
        {
            return currentTimer <= 0 ? interval : currentTimer - deltaTime;
        }

        public int GetSpawnCount()
        {
            return 1;
        }
    }
}