using Game.Components;
using Game.Weapon;
using UnityEngine;

namespace Game.Player
{
    public class PlayerView: MonoBehaviour
    {
        public MoveXYComponent PlayerMove;
        public ShootComponent PlayerShoot;
        public Collider PlayerCollider;
        public HitComponent Hit;
    }
}
