using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService
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
    }
}