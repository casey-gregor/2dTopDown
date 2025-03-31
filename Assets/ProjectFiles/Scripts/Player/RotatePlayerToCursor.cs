// Author: Egor Geisik

using System;
using ProjectFiles.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts.Player
{
    public sealed class RotatePlayerToCursor : ITickable, IDisposable
    {
        private UnityEngine.Camera Camera => UnityEngine.Camera.main;
        private readonly Transform _entityTransform;
        private readonly IGameOverManager  _gameOverManager;
        
        private Vector3 _mouseScreenPosition;
        private Ray _ray;
        private Plane _groundPlane =  new(Vector3.forward, Vector3.zero);

        private bool _gameOver;

        [Inject]
        public RotatePlayerToCursor(
            Transform entityTransform, 
            IGameOverManager gameOverManager)
        {
            _entityTransform = entityTransform;
            _gameOverManager = gameOverManager;
            
            _groundPlane = new Plane(Vector3.forward, Vector3.zero);

            _gameOverManager.OnGameOver += HandleGameOverEvent;
        }

        public void Tick()
        {
            if(_gameOver) {return;}
            RotatePlayerTowardsMouse();
        }
        
        public void Dispose()
        {
            _gameOverManager.OnGameOver -= HandleGameOverEvent;
        }
        
        private void HandleGameOverEvent()
        {
            _gameOver = true;
        }
        
        private void RotatePlayerTowardsMouse()
        {
            _mouseScreenPosition = UnityEngine.Input.mousePosition;

            _ray = Camera.ScreenPointToRay(_mouseScreenPosition);
     
            if (_groundPlane.Raycast(_ray, out var rayLength))
            {
                Vector3 pointLook = _ray.GetPoint(rayLength);
                Vector3 direction = pointLook - _entityTransform.position;
                direction.z = 0;
                
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                _entityTransform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}