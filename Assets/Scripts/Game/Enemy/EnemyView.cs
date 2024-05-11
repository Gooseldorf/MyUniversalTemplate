using System;
using Game.Player;
using Game.Weapon.Laser;
using Infrastructure.AssetManagement;
using Infrastructure.DI;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private LaserWeaponView laserWeaponView;
    [SerializeField] private Collider enemyCollider;
    
    public MoveXYComponent EnemyMove;
    public ShootComponent EnemyShoot;
    private LaserWeaponController laserWeaponController;

    private float shootTimer = 0;
    private float shootTime = 3;
    private CompositeDisposable disposables = new CompositeDisposable(); 

    private void Start()
    {
        GameInstaller installer = FindObjectOfType<GameInstaller>();
        EnemyMove.SetSpeed(10);
        laserWeaponController = new LaserWeaponController(laserWeaponView, installer.Resolve<IAssetProvider>(), false);
        laserWeaponController.Init();
        
        enemyCollider.OnTriggerEnterAsObservable()
            .Where(collision => collision.transform.parent.TryGetComponent(out LaserProjectileView laserProjectile) && laserProjectile.IsPlayerProjectile)
            .Subscribe(_ => OnHit())
            .AddTo(disposables);
    }
    
    private void OnDestroy()
    {
        disposables.Dispose();
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

    private void OnHit()
    {
        Debug.Log("Hit");
    }
}
