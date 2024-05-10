﻿using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{    
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMoveXYComponent : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        private float speed;
        
        public void SetSpeed(float speed) => this.speed = speed < 0 ? 0 : speed;

        public void Move(Vector2 moveVector)
        {
            Vector3 movement = new Vector3(moveVector.normalized.x, moveVector.normalized.y, 0) * speed * Time.deltaTime;
            playerTransform.Translate(movement, Space.World);
        }
    }
}