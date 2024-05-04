using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        float Move1DValue { get; }
        Vector2 Move2DValue { get; }
        Vector2 LookValue { get; }
        bool JumpValue { get; }
        bool SprintValue { get; }
        bool CrouchValue { get; }
        bool AttackValue { get; }
        bool InteractValue { get; }
        bool TestValue { get; }
        
        event Action<float> Move1DEvent;
        event Action<Vector2> Move2DEvent;
        event Action<Vector2> LookEvent;
        event Action JumpEvent;
        event Action SprintEvent;
        event Action CrouchEvent;
        event Action AttackEvent;
        event Action InteractEvent;
        event Action TestEvent;
    }
}