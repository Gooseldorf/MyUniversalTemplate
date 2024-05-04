using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Services.Input
{
    public class InputService: IInputService, IInputEvents, IDisposable
    {
        private readonly Controls controls;

        public InputService()
        {
            controls = new Controls();
            controls.Enable();

            Subscribe();
        }

        public float Move1DValue => controls.Game.Movement1D.ReadValue<float>();
        public Vector2 Move2DValue => controls.Game.Movement2D.ReadValue<Vector2>();
        public Vector2 LookValue => controls.Game.Look.ReadValue<Vector2>();
        public bool JumpValue => controls.Game.Jump.ReadValue<bool>();
        public bool SprintValue => controls.Game.Sprint.ReadValue<bool>();
        public bool CrouchValue => controls.Game.Crouch.ReadValue<bool>();
        public bool AttackValue => controls.Game.Attack.ReadValue<bool>();
        public bool InteractValue => controls.Game.Interact.ReadValue<bool>();
        public bool TestValue => controls.Game.TEST.ReadValue<bool>();
        
        public event Action<float> Move1DEvent;
        public event Action<Vector2> Move2DEvent;
        public event Action<Vector2> LookEvent;
        public event Action JumpEvent;
        public event Action SprintEvent;
        public event Action CrouchEvent;
        public event Action AttackEvent;
        public event Action InteractEvent;
        public event Action TestEvent;
        
        private void Subscribe()
        {
            controls.Game.Movement1D.performed += Move1D;
            controls.Game.Movement2D.performed += Move2D;
            controls.Game.Look.performed += Look;
            controls.Game.Jump.performed += Jump;
            controls.Game.Sprint.performed += Sprint;
            controls.Game.Crouch.performed += Crouch;
            controls.Game.Attack.performed += Attack;
            controls.Game.Interact.performed += Interact;
            controls.Game.TEST.performed += Test;
        }
        
        public void Dispose()
        {
            Unsubscribe();
            controls?.Dispose();
        }

        private void Unsubscribe()
        {
            controls.Game.Movement1D.performed -= Move1D;
            controls.Game.Movement2D.performed -= Move2D;
            controls.Game.Look.performed -= Look;
            controls.Game.Jump.performed -= Jump;
            controls.Game.Sprint.performed -= Sprint;
            controls.Game.Crouch.performed -= Crouch;
            controls.Game.Attack.performed -= Attack;
            controls.Game.Interact.performed -= Interact;
            controls.Game.TEST.performed -= Test;
        }

        private void Move1D(InputAction.CallbackContext context) => Move1DEvent?.Invoke(context.ReadValue<float>());
        private void Move2D(InputAction.CallbackContext context) => Move2DEvent?.Invoke(context.ReadValue<Vector2>());
        private void Look(InputAction.CallbackContext context) => LookEvent?.Invoke(context.ReadValue<Vector2>());
        private void Jump(InputAction.CallbackContext context) { if(context.ReadValueAsButton()) JumpEvent?.Invoke(); }
        private void Sprint(InputAction.CallbackContext context) { if(context.ReadValueAsButton()) SprintEvent?.Invoke(); }
        private void Crouch(InputAction.CallbackContext context) { if(context.ReadValueAsButton()) CrouchEvent?.Invoke(); }
        private void Attack(InputAction.CallbackContext context) { if(context.ReadValueAsButton()) AttackEvent?.Invoke(); }
        private void Interact(InputAction.CallbackContext context) { if(context.ReadValueAsButton()) InteractEvent?.Invoke(); }
        private void Test(InputAction.CallbackContext context) { if(context.ReadValueAsButton()) TestEvent?.Invoke(); }
    }
}