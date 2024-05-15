using Game.Weapon.Laser;
using UnityEngine;

namespace Game.Player
{
    public class ShootComponent : MonoBehaviour
    {
        public void Shoot(LaserWeaponController laserWeaponController)
        {
            laserWeaponController.Shoot();
        }
    }
}