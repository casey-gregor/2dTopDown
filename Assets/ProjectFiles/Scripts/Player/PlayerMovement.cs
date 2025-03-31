// Author: Egor Geisik

using System;
using ProjectFiles.Scripts.Input;
using ProjectFiles.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts.Player
{
    public sealed class PlayerMovement : IFixedTickable, IDisposable
    {
        private readonly PlayerInputController  _playerInputController;
        private readonly IGameOverManager  _gameOverManager;
        private readonly Rigidbody2D  _rigidbody;
        private readonly int _moveSpeed;

        private bool _gameOver;

        [Inject]
        public PlayerMovement(
            PlayerInputController playerInputController,
            IGameOverManager gameOverManager,
            Rigidbody2D rigidbody,
            int moveSpeed)
        {
            _playerInputController = playerInputController;
            _gameOverManager = gameOverManager;
            _rigidbody = rigidbody;
            _moveSpeed = moveSpeed;

            _gameOverManager.OnGameOver += HandleGameOverEvent;
        }

        public void FixedTick()
        {
            if (_gameOver) { return; }
            Vector2 direction = _playerInputController.MoveDirection.normalized;
            _rigidbody.linearVelocity = direction * _moveSpeed;
        }

        public void Dispose()
        {
            _gameOverManager.OnGameOver -= HandleGameOverEvent;
        }
        
        private void HandleGameOverEvent()
        {
            _gameOver = true;
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }
}