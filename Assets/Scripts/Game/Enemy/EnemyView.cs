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
    [SerializeField] private Collider enemyCollider;
    public Vector2 Size { get; private set;}
    
    public MoveXYComponent EnemyMove;
    public ShootComponent EnemyShoot;
    private LaserWeaponController laserWeaponController;
    
    private CompositeDisposable disposables = new CompositeDisposable(); 

    private void Start()
    {
        EnemyMove.SetSpeed(10);

        Size = enemyCollider.bounds.size;
        
        enemyCollider.OnTriggerEnterAsObservable()
            .Where(collision => 
                transform.parent != null &&
                collision.transform.parent.TryGetComponent(out LaserProjectileView laserProjectile) && 
                laserProjectile.IsPlayerProjectile)
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
    }

    private void OnHit()
    {
        Debug.Log("Hit");
    }
}
