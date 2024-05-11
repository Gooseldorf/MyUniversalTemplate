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
        private readonly Bounds gameFieldBounds;
        private readonly IInputService inputService;
        private readonly CompositeDisposable disposes = new();
        private readonly LaserWeaponController weaponController;

        public event Action Dead;

        public PlayerController(PlayerView playerView, Bounds gameFieldBounds, LaserWeaponController weaponController, IInputService inputService)
        {
            this.playerView = playerView;
            this.gameFieldBounds = gameFieldBounds;
            this.weaponController = weaponController;
            this.inputService = inputService;
        }

        public void Init()
        {
            inputService.Move2DStream.Subscribe(Move).AddTo(disposes);
            inputService.AttackStream.Subscribe(Shoot).AddTo(disposes);
            playerView.PlayerCollider.OnTriggerEnterAsObservable().
                Where(collision => 
                    (collision.transform.parent.TryGetComponent(out LaserProjectileView laserProjectile) && !laserProjectile.IsPlayerProjectile) || 
                    collision.transform.parent.TryGetComponent(out EnemyView enemy))
                .Subscribe(_ => Die())
                .AddTo(disposes);
        }

        private void Die()
        {
            Dead?.Invoke();
        }

        public void Dispose() => disposes.Dispose();

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