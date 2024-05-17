using Audio;
using Enums;
using UnityEngine;

namespace Game.Weapon.Laser
{
    public class LaserWeaponController
    {
        private readonly LaserWeaponView view;
        private readonly LaserProjectilePool projectilePool;
        private readonly IAudioManager audioManager;
        private  LaserProjectileFactory factory;
        private readonly bool isPlayerWeapon;
        
        public LaserWeaponController(LaserWeaponView view, LaserProjectilePool projectilePool, IAudioManager audioManager, bool isPlayerWeapon)
        {
            this.view = view;
            this.projectilePool = projectilePool;
            this.audioManager = audioManager;
            this.isPlayerWeapon = isPlayerWeapon;
        }

        public void Shoot()
        {
            ShotPoint[] shotPoints = view.GetNextShootPoints(1);
            
            foreach (var shotPoint in shotPoints)
            {
                LaserProjectileView projectile = projectilePool.Pool.Get();
                var projectileTransform = projectile.transform;
                projectileTransform.position = shotPoint.GetCenterPosition();
                projectileTransform.rotation = shotPoint.GetRotation();
                projectile.Fire(shotPoint.GetDirection(), isPlayerWeapon);
                audioManager.PlayGame2DSound(SoundKeys.LaserGun);
            }
        }
    }
}