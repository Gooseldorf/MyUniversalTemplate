using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputEvents
    {
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