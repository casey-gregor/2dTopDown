// Author: Egor Geisik

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectFiles.Scripts.Input
{
    public sealed class PlayerInputController : IInitializable, IDisposable
    {
        public Vector2 MoveDirection => _moveDirection;
        public event Action OnAttackInput;
        
        private  Vector2 _moveDirection;
        
        private readonly InputAction  _moveAction;
        private readonly InputAction _attackAction;

        private const string MoveInputName = "Move";
        private const string AttackInputName = "Attack";

        [Inject]
        public PlayerInputController(PlayerInput playerInput)
        {
            _moveAction = playerInput.actions[MoveInputName];
            _attackAction = playerInput.actions[AttackInputName];
        }

        public void Initialize()
        {
            _moveAction.performed += ReadMoveInput;
            _moveAction.canceled += CancelMoveInput;

            _attackAction.performed += ReadAttackInput;
        }

        public void Dispose()
        {
            _moveAction.performed -= ReadMoveInput;
            _moveAction.canceled -= CancelMoveInput;
            _attackAction.performed -= ReadAttackInput;
        }
        
        private void ReadAttackInput(InputAction.CallbackContext context)
        {
            OnAttackInput?.Invoke();
        }

        private void ReadMoveInput(InputAction.CallbackContext context)
        {
            _moveDirection = context.ReadValue<Vector2>();
        }
        private void CancelMoveInput(InputAction.CallbackContext context)
        {
            _moveDirection = Vector2.zero;
        }
    }
}