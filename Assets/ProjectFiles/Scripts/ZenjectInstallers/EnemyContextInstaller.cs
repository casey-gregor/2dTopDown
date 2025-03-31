// Author: Egor Geisik

using ProjectFiles.Scripts.HealthBar;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts
{
    public sealed class EnemyContextInstaller :  MonoInstaller
    {
        [SerializeField] private EnemySetupConfig enemySetupConfig;
        [SerializeField] private EnemyWeaponSetupConfig enemyWeaponConfig;
        [SerializeField] private HealthBarView healthBarView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyEntity>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<HealthComponent>()
                .AsSingle()
                .WithArguments(enemySetupConfig.GetData().MaxHealth);

            Container.BindInterfacesAndSelfTo<EnemyMovement>()
                .AsSingle()
                .WithArguments(enemySetupConfig.GetData());
            
            Container.BindInstance(enemySetupConfig.GetData().BulletLayer)
                .WhenInjectedInto<EnemyEntity>();
            
            Container.BindInstance(enemyWeaponConfig.GetData())
                .WhenInjectedInto<EnemyWeapon>();
            
            Container.BindInterfacesAndSelfTo<HealthBarView>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<HealthBarPresenter>()
                .AsSingle().NonLazy();

        }
    }
}