using Infrastructure.AssetManagement;
using Interfaces;
using UniRx;

namespace Game.Weapon.Laser
{
    public class LaserWeaponController
    {
        private readonly LaserWeaponView view;
        private readonly LaserProjectilePool projectilePool;
        private  LaserProjectileFactory factory;
        private readonly bool isPlayerWeapon;
        
        public LaserWeaponController(LaserWeaponView view, LaserProjectilePool projectilePool, bool isPlayerWeapon)
        {
            this.view = view;
            this.projectilePool = projectilePool;
            this.isPlayerWeapon = isPlayerWeapon;
        }

        public void Shoot()
        {
            ShotPoint[] shotPoints = view.GetNextShootPoints(1);//TODO: LaserWeaponData

            foreach (var shotPoint in shotPoints)
            {
                LaserProjectileView projectile = projectilePool.Pool.Get();
                var projectileTransform = projectile.transform;
                projectileTransform.position = shotPoint.GetCenterPosition();
                projectileTransform.rotation = shotPoint.GetRotation();
                projectile.Fire(shotPoint.GetDirection(), isPlayerWeapon);
            }
        }
    }
}