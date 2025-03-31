using UnityEngine;

namespace ProjectFiles.Scripts.Character.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerSetupConfig", menuName = "PlayerSetup/PlayerSetupConfig", order = 0)]
    public sealed class PlayerSetupConfig : ScriptableObject
    {
        [SerializeField] private int moveSpeed = 5;
        [SerializeField] private int maxHealth;
        
        public  int MoveSpeed => moveSpeed;
        public  int MaxHealth => maxHealth;
    }
}