// Author: Egor Geisik

using System;
using System.Collections.Generic;
using ProjectFiles.Scripts.Input;
using ProjectFiles.Scripts.Interfaces;
using ProjectFiles.Scripts.Player.Weapon;
using ProjectFiles.Scripts.Weapon.Config;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts.Weapon
{
    public sealed class PlayerWeapon : IInitializable, IDisposable, IFixedTickable
    {
        private readonly PlayerInputController _inputController;
        private readonly Bullet.BulletPool _bulletPool;
        private readonly Transform _firePoint;
        private readonly IGameOverManager _gameOverManager;
        private readonly List<Bullet> _activeBullets = new();
        private readonly IBulletMovementStrategy _bulletMovementStrategy;

        private readonly int _damage;
        private readonly float _bulletSpeed;
        private readonly float _distance;
        private bool _gameOver;

        [Inject]
        public PlayerWeapon(
            PlayerInputController playerInputController, 
            Bullet.BulletPool bulletPool, 
            Transform firePoint,
            WeaponSetupData weaponSetupData, 
            IGameOverManager gameOverManager, 
            IBulletMovementStrategy bulletMovementStrategy)
        {
            _inputController  = playerInputController;
            _bulletPool = bulletPool;
            _firePoint = firePoint;
            _gameOverManager = gameOverManager;
            _bulletMovementStrategy = bulletMovementStrategy;
            _bulletSpeed = weaponSetupData.BulletSpeed;
            _damage = weaponSetupData.Damage;
            _distance = weaponSetupData.Distance;
            
            _gameOverManager.OnGameOver += HandleGameOverEvent;
        }
        
        public void Initialize()
        {
            _inputController.OnAttackInput += SpawnBullet;
        }

        public void Dispose()
        {
            _inputController.OnAttackInput -= SpawnBullet;
            _gameOverManager.OnGameOver -= HandleGameOverEvent;
        }
        
        public void FixedTick()
        {
            if(_gameOver) {return;}
            
            for (int i = 0; i < _activeBullets.Count; i++)
            {
                Bullet bullet = _activeBullets[i];
                bullet.Move();
                bullet.CheckDistance();
            }
        }
        
        private void HandleGameOverEvent()
        {
            _gameOver = true;
            _inputController.OnAttackInput -= SpawnBullet;
        }

        private void SpawnBullet()
        {
            Bullet bullet = _bulletPool.Spawn();
            bullet.InitializeBullet(
                movementStrategy: _bulletMovementStrategy,
                firePoint: _firePoint, 
                speed: _bulletSpeed, 
                distance: _distance,
                damage: _damage);
            bullet.OnDestroyEvent += DespawnBullet;
            _activeBullets.Add(bullet);
        }

        private void DespawnBullet(Bullet bullet)
        {
            bullet.OnDestroyEvent -= DespawnBullet;
            _activeBullets.Remove(bullet);
            _bulletPool.Despawn(bullet);
        }
    }
}