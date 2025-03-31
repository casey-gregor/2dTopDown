using UnityEngine;

namespace ProjectFiles.Scripts.Weapon.Config
{
    [CreateAssetMenu(fileName = "WeaponSetupConfig", menuName = "Player/Weapon/WeaponSetup", order = 0)]
    public sealed class PlayerWeaponSetupConfig : ScriptableObject
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float bulletSpeed = 20f;
        [SerializeField] private int damage = 50;
        [SerializeField] private float distance = 10f;

        public WeaponSetupData GetData()
        {
            return new WeaponSetupData(
                bulletPrefab, 
                bulletSpeed, 
                damage, 
                distance);
        }
    }

    public struct WeaponSetupData
    {
        public Bullet BulletPrefab { get; private set; }
        public float BulletSpeed { get; private set; }
        public int Damage { get; private set; }
        public float Distance { get; private set; }

        public WeaponSetupData(
            Bullet bulletPrefab, 
            float bulletSpeed, 
            int damage, 
            float distance)
        {
            BulletPrefab = bulletPrefab;
            BulletSpeed = bulletSpeed;
            Damage = damage;
            Distance = distance;
        }
    }
}