// Author: Egor Geisik

using System;
using ProjectFiles.Scripts.Common;
using ProjectFiles.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts.GameCycle
{
    public sealed class GameOverManager : IDisposable, IGameOverManager
    {
        public event Action OnGameOver;
        private readonly IPlayer _player;
        private readonly IEnemySpawner _enemySpawner;
        private readonly GameOverView _gameOverView;

        [Inject]
        public GameOverManager(
            IPlayer player, 
            IEnemySpawner enemySpawner, 
            GameOverView gameOverView)
        {
            _player = player;
            _enemySpawner = enemySpawner;
            _gameOverView = gameOverView;

            _player.OnPlayerDeath += HandleGameOver;
            _enemySpawner.OnAllKilled += HandleGameOver;
        }

        public void Dispose()
        {
            _player.OnPlayerDeath -= HandleGameOver;
            _enemySpawner.OnAllKilled -= HandleGameOver;
        }
        
        private void HandleGameOver()
        {
            OnGameOver?.Invoke();
            _gameOverView.Show();
            Debug.Log("Game Over");
        }
    }
}