using System;
using UnityEngine;

namespace Game.Components
{
    public class HitComponent : MonoBehaviour
    {
        public event Action<bool> OnHit;

        public void Hit(bool isPlayerProjectile) => OnHit?.Invoke(isPlayerProjectile);
    }
}