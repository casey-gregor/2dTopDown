using ProjectFiles.Scripts;
using ProjectFiles.Scripts.Character.ScriptableObjects;
using ProjectFiles.Scripts.HealthBar;
using ProjectFiles.Scripts.Player;
using UnityEngine;
using Zenject;

public sealed class PlayerContextInstaller : MonoInstaller
{
    [SerializeField] private PlayerSetupConfig playerSetupConfig;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D playerRigidbody;
    
    public override void InstallBindings()
    {
        if (playerTransform == null)
        {
            Debug.LogError("playerTransform is null in PlayerContextInstaller!");
        }
        
        Container.BindInterfacesAndSelfTo<PlayerEntity>()
            .FromComponentsInHierarchy()
            .AsSingle();
        
        Container.BindInterfacesAndSelfTo<HealthComponent>()
            .AsSingle()
            .WithArguments(playerSetupConfig.MaxHealth);
        
        Container.BindInterfacesAndSelfTo<RotatePlayerToCursor>()
            .AsSingle()
            .WithArguments(playerTransform);

        Container.BindInterfacesAndSelfTo<PlayerMovement>()
            .AsSingle()
            .WithArguments(playerRigidbody, playerSetupConfig.MoveSpeed);

        Container.BindInterfacesAndSelfTo<HealthBarView>()
            .FromComponentInHierarchy()
            .AsSingle();

        Container.BindInterfacesAndSelfTo<HealthBarPresenter>()
            .AsSingle().NonLazy();
    }
}
