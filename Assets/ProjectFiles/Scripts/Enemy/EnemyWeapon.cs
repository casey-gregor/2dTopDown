using ProjectFiles.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts
{
    public sealed class EnemyWeapon : MonoBehaviour
    {
        private Collider2D _playerCollider;
        private int _damage;
        private int _playerLayer;
        private float _damageTimerSet;
        private float _damageTimer;

        [Inject]
        private void Construct(EnemyWeaponSetupData data)
        {
            _damage = data.Damage;
            _damageTimerSet = data.DamageInterval;
            _playerLayer = data.PlayerLayer.value;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                _playerCollider = other;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (((1 << other.gameObject.layer) & _playerLayer) != 0)
            {
                _playerCollider = null;
                _damageTimer = 0;
            }
        }
        
        private void DamagePlayer(Collider2D other)
        {
            if (other.TryGetComponent<IPlayer>(out var player) && !player.IsDead)
            {
                player.HealthComponent.TakeDamage(_damage);
                _damageTimer = _damageTimerSet;
            }
        }

        private void Update()
        {
            if (_playerCollider != null)
            {
                _damageTimer -= Time.deltaTime;
                if (_damageTimer <= 0f)
                {
                    DamagePlayer(_playerCollider);
                }
            }
        }
    }
}