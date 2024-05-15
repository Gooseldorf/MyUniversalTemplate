﻿using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemies/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string Address;
        public int Health;
        public float ShootDelay;
    }
}