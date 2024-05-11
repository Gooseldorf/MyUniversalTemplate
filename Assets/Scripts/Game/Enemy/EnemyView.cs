using Game.Player;
using Game.Weapon.Laser;
using Infrastructure.AssetManagement;
using Infrastructure.DI;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private LaserWeaponView laserWeaponView;
    public MoveXYComponent EnemyMove;
    public ShootComponent EnemyShoot;
    private LaserWeaponController laserWeaponController;

    private float shootTimer = 0;
    private float shootTime = 3;

    private void Start()
    {
        GameInstaller installer = FindObjectOfType<GameInstaller>();
        EnemyMove.SetSpeed(10);
        laserWeaponController = new LaserWeaponController(laserWeaponView, installer.Resolve<IAssetProvider>());
        laserWeaponController.Init();
    }

    private void Update()
    {
        EnemyMove.Move(Vector2.down);

        shootTimer += Time.deltaTime;
        if (shootTimer > shootTime)
        {
            shootTimer = 0;
            EnemyShoot.Shoot(laserWeaponController);
        }
    }
}
