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
    public Collider EnemyCollider;
    public Vector2 Size { get; private set;}
    
    public MoveXYComponent EnemyMove;
    public ShootComponent EnemyShoot;
    private LaserWeaponController laserWeaponController;
    
    private CompositeDisposable disposables = new CompositeDisposable(); 

    private void Start()
    {
        EnemyMove.SetSpeed(20);
        Size = EnemyCollider.bounds.size;
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
