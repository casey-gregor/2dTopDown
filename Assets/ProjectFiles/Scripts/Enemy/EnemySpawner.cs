using System;
using System.Collections.Generic;
using ProjectFiles.Scripts.Interfaces;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ProjectFiles.Scripts
{
    public sealed class EnemySpawner : MonoBehaviour, IEnemySpawner
    {
        public event Action OnAllKilled;
        
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField, Min(0)] private float spawnInterval;
        [SerializeField, Min(0)] private int enemiesToSpawn;

        private readonly List<IEnemy> _activeEnemies = new();
        private EnemyEntity.Pool _enemyPool;
        private IGameOverManager _gameOverManager;
        private ISpawnStrategy _spawnStrategy;

        private float _timer;
        private bool _gameOver;

        [Inject]
        private void Construct(
            EnemyEntity.Pool enemyPool, 
            IGameOverManager  gameOverManager, 
            ISpawnStrategy spawnStrategy)
        {
            _enemyPool = enemyPool;
            _gameOverManager = gameOverManager;
            _spawnStrategy = spawnStrategy;
        }

        private void Awake()
        {
            if (spawnPoints.Length == 0)
            {
                Debug.LogError("Spawn points array is empty");
            }

            if (spawnInterval == 0)
            {
                Debug.LogError("Spawn interval is zero");
            }
            
            if (enemiesToSpawn == 0)
            {
                Debug.LogError("Enemies to spawn is zero");
            }
            
            _timer = spawnInterval;
            _gameOver = false;
            
            _gameOverManager.OnGameOver += HandleGameOver;
        }

        private void OnDestroy()
        {
            _gameOverManager.OnGameOver -= HandleGameOver;
        }

        private void HandleGameOver()
        {
            _gameOver = true;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            CheckIfAllKilled();
            TrySpawnEnemy(deltaTime);
        }

        private void TrySpawnEnemy(float deltaTime)
        {
            if (_spawnStrategy.ShouldSpawn(_timer, enemiesToSpawn, _gameOver))
            {
                int spawnCount = _spawnStrategy.GetSpawnCount();
                for (int i = 0; i < spawnCount && enemiesToSpawn > 0; i++)
                {
                    InstantiateEnemy();
                    enemiesToSpawn--;
                }
            }
            _timer = _spawnStrategy.GetNextTimer(_timer, spawnInterval, deltaTime);
        }

        private void InstantiateEnemy()
        {
            EnemyEntity enemyEntity = _enemyPool.Spawn();
            _activeEnemies.Add(enemyEntity);
            enemyEntity.OnDeath += DespawnEnemy;
            enemyEntity.transform.position = GetRandomSpawnPoint();
        }

        private void DespawnEnemy(EnemyEntity enemyEntity)
        {
            enemyEntity.OnDeath -= DespawnEnemy;
            _activeEnemies.Remove(enemyEntity);
            _enemyPool.Despawn(enemyEntity);
        }

        private Vector2 GetRandomSpawnPoint()
        {
            int randomPoint = Random.Range(0, spawnPoints.Length);
            return spawnPoints[randomPoint].position;
        }

        private void CheckIfAllKilled()
        {
            if (enemiesToSpawn == 0 && _activeEnemies.Count == 0 && !_gameOver)
            {
                OnAllKilled?.Invoke();
            }
        }
    }
}