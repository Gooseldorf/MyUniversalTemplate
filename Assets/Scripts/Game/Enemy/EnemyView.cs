using Game.Components;
using Game.Player;
using Game.Weapon;
using Game.Weapon.Laser;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Collider EnemyCollider;
    public HitComponent HitComponent;
    public MoveXYComponent EnemyMove;
    public ShootComponent EnemyShoot;
    public LaserWeaponView EnemyLaserWeapon;
    
    public Vector2 Size { get; private set;}
    
    private void Start()
    {
        EnemyMove.SetSpeed(20);
        Size = EnemyCollider.bounds.size;
    }

    private void Update()
    {
        EnemyMove.Move(Vector2.down);
    }
}
