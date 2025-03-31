// Author: Egor Geisik

using UnityEngine;

namespace ProjectFiles.Scripts
{
    [CreateAssetMenu(fileName = "EnemyWeaponSetupConfig", menuName = "EnemySetup/WeaponSetupConfig")]
    public sealed class EnemyWeaponSetupConfig : ScriptableObject
    {
        [SerializeField] private int damage;
        [SerializeField] private float damageInterval;
        [SerializeField] private LayerMask playerLayer;

        public EnemyWeaponSetupData GetData()
        {
            return new EnemyWeaponSetupData(
                damage, 
                damageInterval, 
                playerLayer);
        }
    }

    public struct EnemyWeaponSetupData
    {
        public int Damage { get; private set; }
        public float DamageInterval { get; private set; }
        public LayerMask PlayerLayer { get; private set; }
        
        public EnemyWeaponSetupData(
            int damage, 
            float damageInterval, 
            LayerMask playerLayer)
        {
            Damage = damage;
            DamageInterval = damageInterval;
            PlayerLayer = playerLayer;
        }
    }
}