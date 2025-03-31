using ProjectFiles.Scripts.Player.Weapon;
using ProjectFiles.Scripts.Weapon.Config;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts.Weapon
{
    public sealed class PlayerWeaponInstaller : MonoInstaller
    {
        [SerializeField] private PlayerWeaponSetupConfig playerWeaponSetupConfig;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform bulletsParent;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerWeapon>()
                .AsSingle()
                .WithArguments(firePoint, playerWeaponSetupConfig.GetData());
            
            Container.BindMemoryPool<Bullet, Bullet.BulletPool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(playerWeaponSetupConfig.GetData().BulletPrefab)
                .UnderTransform(bulletsParent);
            
            Container.BindInterfacesAndSelfTo<StraightBulletMovementStrategy>()
                .AsSingle();
        }
    }
}