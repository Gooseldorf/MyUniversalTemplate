using UnityEngine;

namespace Game.Components
{
    [RequireComponent(typeof(CharacterController))] 
    public class MoveWithCCComponent : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        
        public void Move(Vector2 moveVector) => controller.Move(new Vector3(moveVector.x, 0, moveVector.y) * 10 * Time.deltaTime);
    }
}