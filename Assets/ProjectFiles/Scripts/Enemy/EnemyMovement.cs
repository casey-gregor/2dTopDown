// Author: Egor Geisik

using ProjectFiles.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts
{
    public sealed class EnemyMovement : IEnemyMovement
    {
        private readonly Transform _rootTransform;
        private readonly Transform _playerTransform;
        private readonly float _moveSpeed;
        private readonly float _attackDistance;
        private readonly float _circleDistance; 

        private bool _isBehindPlayer;
        private const float BehindAngleThreshold = 90f;
        private const float DistanceBuffer = 0.1f;
        private const float RetreatMultiplier = 1.5f;

        [Inject]
        public EnemyMovement(
            IPlayer player, 
            IEnemy enemy, 
            EnemySetupData enemySetupData)
        {
            _playerTransform = player.PlayerTransform;
            _rootTransform = enemy.EnemyTransform;
            _moveSpeed = enemySetupData.MoveSpeed;
            _attackDistance = enemySetupData.StopDistance;
            _circleDistance = enemySetupData.CircleDistance;
        }

        public void Move(float deltaTime)
        {
            Vector2 directionToPlayer = _playerTransform.position - _rootTransform.position;
            
            float distanceToPlayer = directionToPlayer.magnitude;

            RotateToPlayer(directionToPlayer);

            _isBehindPlayer = IsEnemyBehindPlayer();

            if (_isBehindPlayer)
            {
                if (distanceToPlayer > _attackDistance)
                {
                    MoveTowardsPlayer(deltaTime);
                }
                // If within attack distance, stop and attack
            }
            else
            {
                MoveToGetBehind(deltaTime);
            }
        }
        
        private bool IsEnemyBehindPlayer()
        {
            Vector2 playerForward = _playerTransform.up;
            Vector2 toEnemy = _rootTransform.position - _playerTransform.position;
            float angle = Vector2.SignedAngle(playerForward, toEnemy);
            return (angle > BehindAngleThreshold || angle < -BehindAngleThreshold);
        }

        private void MoveTowardsPlayer(float deltaTime)
        {
            _rootTransform.position = Vector2.MoveTowards(
                _rootTransform.position,
                _playerTransform.position,
                _moveSpeed * deltaTime
            );
        }

        private void MoveToGetBehind(float deltaTime)
        {
            // Calculate current direction and distance from player to enemy
            Vector2 directionFromPlayerToSelf = (Vector2)_rootTransform.position - (Vector2)_playerTransform.position;
            float distanceToPlayer = directionFromPlayerToSelf.magnitude;

            // Calculate target position with circular motion
            Vector2 perpendicular = new Vector2(-directionFromPlayerToSelf.y, directionFromPlayerToSelf.x).normalized;
            Vector2 circleDirection = Vector2.SignedAngle(directionFromPlayerToSelf, -(Vector2)_playerTransform.up) > 0 ? perpendicular : -perpendicular;
            Vector2 targetPosition = (Vector2)_playerTransform.position - (Vector2)_playerTransform.up * _circleDistance + circleDirection * _circleDistance;

            // Adjust target position based on distance conditions
            Vector2 directionToTarget = targetPosition - (Vector2)_playerTransform.position;
            float distanceToTarget = directionToTarget.magnitude;
            float targetDistance = (distanceToPlayer <= _attackDistance + DistanceBuffer) ? _attackDistance * RetreatMultiplier : 
                (distanceToTarget < _attackDistance) ? _attackDistance : distanceToTarget;
            targetPosition = (Vector2)_playerTransform.position + directionToTarget.normalized * targetDistance;

            // Move towards target, clamping to attack distance
            Vector2 newPosition = Vector2.MoveTowards(_rootTransform.position, targetPosition, _moveSpeed * deltaTime);
            if ((newPosition - (Vector2)_playerTransform.position).magnitude < _attackDistance)
            {
                newPosition = (Vector2)_playerTransform.position + (newPosition - (Vector2)_playerTransform.position).normalized * _attackDistance;
            }

            _rootTransform.position = newPosition;
        }

        private void RotateToPlayer(Vector2 directionToPlayer)
        {
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - BehindAngleThreshold;
            _rootTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}