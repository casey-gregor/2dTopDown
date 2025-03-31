namespace ProjectFiles.Scripts.Interfaces
{
    public interface ISpawnStrategy
    {
        public bool ShouldSpawn(float timer, int remainingEnemies, bool gameOver);
        public float GetNextTimer(float currentTimer, float interval, float deltaTime); 
        public int GetSpawnCount();
    }
}