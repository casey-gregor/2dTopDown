// Author: Egor Geisik
using System;
using ProjectFiles.Scripts.Player.Weapon;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnDestroyEvent;
        public int Damage => _damage;
        
        private IBulletMovementStrategy _movementStrategy;
        private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;
        private Vector2 _startPosition;

        private float _moveSpeed;
        private float _distance;
        private int _damage;
        
        public void InitializeBullet(
            IBulletMovementStrategy movementStrategy,   
            Transform firePoint, 
            float speed, 
            float distance, 
            int damage)
        {
            _movementStrategy = movementStrategy;
            _moveDirection = firePoint.up;
            _moveSpeed = speed;
            _distance = distance;
            _damage = damage;
            transform.position = firePoint.position;
            _startPosition = transform.position;
            transform.parent = null;
        }

        public void Move()
        {
            _rigidbody.linearVelocity = _moveDirection * _moveSpeed;
        }

        public void CheckDistance()
        {
            float distanceTraveledSquared = ((Vector2)transform.position - _startPosition).sqrMagnitude;
            
            if (distanceTraveledSquared >= _distance*_distance)
            {
                DestroyBullet();
            }
        }

        public void DestroyBullet()
        {
            OnDestroyEvent?.Invoke(this);
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public class BulletPool : MonoMemoryPool<Bullet>{}
    }
}