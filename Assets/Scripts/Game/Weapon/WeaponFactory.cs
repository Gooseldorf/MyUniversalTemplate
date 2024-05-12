using Cysharp.Threading.Tasks;
using Game.Weapon.Laser;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;

namespace Game.Weapon
{
    public class WeaponFactory : FactoryBase, IWeaponFactory
    {
        private GameObject playerLaserWeaponPrefab;
        private GameObject enemyLaserWeaponPrefab;
        
        public WeaponFactory(IAssetProvider assetProvider) : base(assetProvider)
        { }

        public override async UniTask WarmUpIfNeeded()
        {
            playerLaserWeaponPrefab = await CachePrefab("PlayerLaserWeapon");
            enemyLaserWeaponPrefab = await CachePrefab("EnemyLaserWeapon");
        }

        public LaserWeaponView CreatePlayerLaserWeapon(Transform parent)
        {
            GameObject laserWeapon =  CreateGameObject(playerLaserWeaponPrefab);
            laserWeapon.transform.SetParent(parent.transform);
            laserWeapon.transform.localPosition = Vector3.zero;
            laserWeapon.TryGetComponent(out LaserWeaponView laserWeaponView);
            return laserWeaponView;
        }

        public LaserWeaponView CreateEnemyLaserWeapon(Transform parent)
        {
            GameObject laserWeapon = CreateGameObject(enemyLaserWeaponPrefab);
            laserWeapon.transform.SetParent(parent.transform);
            laserWeapon.transform.localPosition = Vector3.zero;
            laserWeapon.TryGetComponent(out LaserWeaponView laserWeaponView);
            return laserWeaponView;
        }
    }

    public interface IWeaponFactory
    {
        UniTask WarmUpIfNeeded();
        LaserWeaponView CreatePlayerLaserWeapon(Transform parent);
        LaserWeaponView CreateEnemyLaserWeapon(Transform parent);
    }
}