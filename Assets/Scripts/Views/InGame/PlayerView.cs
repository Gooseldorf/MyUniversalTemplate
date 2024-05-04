using System;
using Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

namespace Views
{
    [RequireComponent(typeof(CharacterController))] 
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerView: MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private PlayerInput input;
        private IInputService inputService;

        private void Start()
        {
            inputService = new InputService();//AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (inputService.Move2DValue != Vector2.zero)
            {
                Move(inputService.Move2DValue);
            }
        }

        private void OnDestroy()
        {
            inputService.Move2DEvent -= Move;
        }

        private void Move(Vector2 moveVector) => controller.Move(new Vector3(moveVector.x, 0, moveVector.y) * Time.deltaTime);
        
    }
}