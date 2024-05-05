using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        IObservable<float> Move1DStream { get; }
        IObservable<Vector2> Move2DStream { get; }
        IObservable<Vector2> LookStream { get; }
        IObservable<bool> JumpStream { get; }
        IObservable<bool> SprintStream { get; }
        IObservable<bool> CrouchStream { get; }
        IObservable<bool> AttackStream { get; }
        IObservable<bool> InteractStream { get; }
        IObservable<bool> TestStream { get; }
        
        void Dispose();
    }
}