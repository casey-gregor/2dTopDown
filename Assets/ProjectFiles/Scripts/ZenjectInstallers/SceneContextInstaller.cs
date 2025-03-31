using ProjectFiles.Scripts;
using ProjectFiles.Scripts.Common;
using ProjectFiles.Scripts.Common.ScriptableObjects;
using ProjectFiles.Scripts.GameCycle;
using ProjectFiles.Scripts.Input;
using ProjectFiles.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Zenject
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity playerEntity;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Transform enemiesParent;
        [SerializeField] private SceneSetupConfig sceneSetupConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerEntity>()
                .FromInstance(playerEntity);
            
            Container.Bind<PlayerInput>()
                .FromInstance(playerInput)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerInputController>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<IntervalSpawnStrategy>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemySpawner>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container.BindMemoryPool<EnemyEntity, EnemyEntity.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(sceneSetupConfig.EnemyEntityPrefab)
                .UnderTransform(enemiesParent);
            
            Container.BindInterfacesAndSelfTo<GameOverManager>()
                .AsSingle();
            
            Container.Bind<GameOverView>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.BindInstance(sceneSetupConfig.BulletLayerMask)
                .WhenInjectedInto<LevelBoundsChecker>();
        }
    }
}