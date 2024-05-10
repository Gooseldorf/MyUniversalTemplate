using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(CharacterController))] 
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMoveWithCCComponent : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        
        public void Move(Vector2 moveVector) => controller.Move(new Vector3(moveVector.x, 0, moveVector.y) * 10 * Time.deltaTime);
    }
}