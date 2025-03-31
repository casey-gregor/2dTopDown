using System;
using ProjectFiles.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts
{
    public sealed class EnemyEntity : MonoBehaviour, IEnemy
    {
        public event Action<EnemyEntity> OnDeath;
        public Transform EnemyTransform => transform;
        
        private IHealthComponent _healthComponent;
        private IEnemyMovement _enemyMovement;
        private IGameOverManager  _gameOverManager;

        private int _bulletLayer;
        private bool _canMove;
        
        [Inject]
        private void Construct(
            IHealthComponent healthComponent,
            IEnemyMovement enemyMovement,
            IGameOverManager  gameOverManager,
            int bulletLayer)
        {
            _healthComponent = healthComponent;
            _enemyMovement = enemyMovement;
            _gameOverManager = gameOverManager;
            _bulletLayer = bulletLayer;
        }
        
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            if(!_canMove){return;}
            _enemyMovement.Move(deltaTime);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1<<other.gameObject.layer) & _bulletLayer) != 0)
            {
                Bullet bullet = other.gameObject.GetComponent<Bullet>();
                _healthComponent.TakeDamage(bullet.Damage);
                bullet.DestroyBullet();
            }
        }
        
        private void HandleGameOver()
        {
            _canMove = false;
        }

        private void HandleDeathEvent()
        {
            OnDeath?.Invoke(this);
        }
        
        private void OnEnable()
        {
            _canMove = true;
            _healthComponent.RestoreHealth();
            _healthComponent.OnDeath += HandleDeathEvent;
            _gameOverManager.OnGameOver += HandleGameOver; 
        }

        private void OnDisable()
        {
            _healthComponent.OnDeath -= HandleDeathEvent;
            _gameOverManager.OnGameOver -= HandleGameOver;
        }
        
        public class Pool : MonoMemoryPool<EnemyEntity>
        {
        }
    }
}