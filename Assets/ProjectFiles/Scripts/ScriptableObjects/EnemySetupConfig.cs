using UnityEngine;

namespace ProjectFiles.Scripts
{
    [CreateAssetMenu(fileName = "EnemySetupConfig", menuName = "EnemySetup/EnemySetupConfig", order = 0)]
    public sealed class EnemySetupConfig : ScriptableObject
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float stopDistance;
        [SerializeField] private float circleDistance;
        [SerializeField] private LayerMask bulletLayer;

        public EnemySetupData GetData()
        {
            return new EnemySetupData(
                maxHealth, 
                moveSpeed, 
                stopDistance, 
                circleDistance,
                bulletLayer.value);
        }
    }

    public struct EnemySetupData
    {
        public int MaxHealth { get; private set; }
        public float MoveSpeed { get; private set; }
        public float StopDistance { get; private set; }
        public float CircleDistance { get; private set; }
        public int BulletLayer { get; private set; }

        public EnemySetupData(
            int maxHealth, 
            float speed, 
            float stopDistance,  
            float circleDistance,
            int bulletLayer)
        {
            MaxHealth = maxHealth;
            MoveSpeed = speed;
            StopDistance = stopDistance;
            CircleDistance = circleDistance;
            BulletLayer = bulletLayer;
        }
    }
}