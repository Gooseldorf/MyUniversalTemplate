using Cysharp.Threading.Tasks;
using Game.Weapon.Laser;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Game.Weapon
{
    public class WeaponFactory : IWeaponFactory
    {
        private readonly IAssetProvider assetProvider;

        public WeaponFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        public async UniTask<LaserWeaponView> CreateLaserWeapon()
        {
            GameObject laserWeapon = await assetProvider.InstantiateAddressable("LaserWeapon");
            laserWeapon.TryGetComponent(out LaserWeaponView laserWeaponView);
            return laserWeaponView;
        }
    }

    public interface IWeaponFactory
    {
        UniTask<LaserWeaponView> CreateLaserWeapon();
    }
}