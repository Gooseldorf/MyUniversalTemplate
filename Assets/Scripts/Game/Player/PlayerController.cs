using System;
using Game.Weapon.Laser;
using Infrastructure.Services.Input;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : IPlayerController
    {
        private readonly PlayerView playerView;
        private readonly IInputService inputService;
        
        private Bounds gameFieldBounds;
        private readonly CompositeDisposable disposes = new();
        private LaserWeaponController weaponController;

        public event Action Dead;
        public Transform ViewTransform => playerView.transform;

        public PlayerController(PlayerView playerView, IInputService inputService)
        {
            this.playerView = playerView;
            this.inputService = inputService;
        }

        public void Init()
        {
            inputService.Move2DStream.Subscribe(Move).AddTo(disposes);
            inputService.AttackStream.Subscribe(Shoot).AddTo(disposes);
            playerView.PlayerCollider.OnCollisionEnterAsObservable().
                Where(collision => 
                    collision.transform.parent != null 
                    && (IsHitByEnemyProjectile(collision) || IsHitByEnemy(collision)))
                .Subscribe(_ => OnHit(false))
                .AddTo(disposes);
            playerView.Hit.HitStream.Subscribe(OnHit).AddTo(disposes);
        }

        public void Dispose()
        {
            disposes.Dispose();
        }

        public void SetMovementBounds(Bounds bounds) => gameFieldBounds = bounds;

        public void SetPosition(Vector3 position) => playerView.transform.position = position;

        public void SetSpeed(float speed) => playerView.PlayerMove.SetSpeed(speed);

        public void SetWeapon(LaserWeaponController laserWeaponController) => weaponController = laserWeaponController;

        public void Reset()
        {
            playerView.transform.position = new Vector3(0, -50, 0);
        }

        private bool IsHitByEnemy(Collision collision) => collision.transform.parent.TryGetComponent(out EnemyView enemy);

        private bool IsHitByEnemyProjectile(Collision collision) => (collision.transform.parent.TryGetComponent(out LaserProjectileView laserProjectile) && !laserProjectile.IsPlayerProjectile);

        private void OnHit(bool isPlayerProjectile)
        {
            if(isPlayerProjectile) return;
            Dead?.Invoke();
            Debug.Log("Dead");
        }

        private void Move(Vector2 moveVector)
        {
            playerView.PlayerMove.Move(moveVector);
            
            var playerBounds = playerView.PlayerCollider.bounds;

            CheckGameBound(playerBounds);
        }

        private void CheckGameBound(Bounds playerBounds)
        {
            if (!gameFieldBounds.Contains(playerBounds.min) || !gameFieldBounds.Contains(playerBounds.max))
            {
                var newPlayerPosition = playerView.transform.position;
                newPlayerPosition.x = Mathf.Clamp(newPlayerPosition.x, gameFieldBounds.min.x + playerBounds.extents.x, gameFieldBounds.max.x - playerBounds.extents.x);
                newPlayerPosition.y = Mathf.Clamp(newPlayerPosition.y, gameFieldBounds.min.y + playerBounds.extents.y, gameFieldBounds.max.y - playerBounds.extents.y);

                playerView.transform.position = newPlayerPosition;
            }
        }

        private void Shoot(bool performed) => playerView.PlayerShoot.Shoot(weaponController);
    }
}