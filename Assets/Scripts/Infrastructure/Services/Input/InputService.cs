using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class InputService: IInputService
    {
        private readonly Controls controls;

        public IObservable<float> Move1DStream { get; private set; }
        public IObservable<Vector2> Move2DStream { get; private set; }
        public IObservable<Vector2> LookStream { get; private set;}
        public IObservable<bool> JumpStream { get; private set; }
        public IObservable<bool> SprintStream { get; private set;}
        public IObservable<bool> CrouchStream { get; private set;}
        public IObservable<bool> AttackStream { get; private set;}
        public IObservable<bool> InteractStream { get; private set;}
        public IObservable<bool> TestStream { get; private set;}
        public IObservable<bool> PauseStream { get; private set; }

        public InputService()
        {
            controls = new Controls();
            controls.Enable();

            CreateStreams();
        }

        private void CreateStreams()
        {
            //Continuous 
            Move1DStream = controls.Game.Movement1D.GenerateContinuousObservable(() => controls.Game.Movement1D.ReadValue<float>());
            Move2DStream = controls.Game.Movement2D.GenerateContinuousObservable(() => controls.Game.Movement2D.ReadValue<Vector2>());
            LookStream = controls.Game.Look.GenerateContinuousObservable(() => controls.Game.Look.ReadValue<Vector2>());
            //Perform & Cancel
            AttackStream = controls.Game.Attack.GeneratePerformObservable((ctx) => controls.Game.Attack.ReadValue<float>() > 0);
            JumpStream = controls.Game.Jump.GeneratePerformObservable((ctx) => controls.Game.Jump.ReadValue<float>() > 0);
            SprintStream = controls.Game.Sprint.GeneratePerformObservable((ctx) => controls.Game.Sprint.ReadValue<float>() > 0);
            CrouchStream = controls.Game.Crouch.GeneratePerformObservable((ctx) => controls.Game.Crouch.ReadValue<float>() > 0);
            InteractStream = controls.Game.Interact.GeneratePerformObservable((ctx) => controls.Game.Interact.ReadValue<float>() > 0);
            TestStream = controls.Game.TEST.GeneratePerformObservable((ctx) => controls.Game.TEST.ReadValue<float>() > 0);
            PauseStream = controls.Game.Pause.GeneratePerformObservable((ctx) => controls.Game.Pause.ReadValue<float>() > 0);
        }
        
        public void Dispose() => controls?.Dispose();
    }
}