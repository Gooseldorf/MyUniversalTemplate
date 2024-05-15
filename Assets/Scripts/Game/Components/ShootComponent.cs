using Game.Weapon.Laser;
using UnityEngine;

namespace Game.Components
{
    public class ShootComponent : MonoBehaviour
    {
        public void Shoot(LaserWeaponController laserWeaponController)
        {
            laserWeaponController.Shoot();
        }
    }
}