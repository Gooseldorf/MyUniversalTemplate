using Game.Weapon.Laser;
using Infrastructure.Services.Input;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : IPlayerController
    {
        private readonly PlayerView playerView;
        private readonly IInputService inputService;
        private readonly CompositeDisposable disposes = new();
        private readonly LaserWeaponController weaponController;

        public PlayerController(PlayerView playerView, LaserWeaponController weaponController, IInputService inputService)
        {
            this.playerView = playerView;
            this.weaponController = weaponController;
            this.inputService = inputService;
        }

        public void Init()
        {
            inputService.Move2DStream.Subscribe(Move).AddTo(disposes);
            inputService.AttackStream.Subscribe(Shoot).AddTo(disposes);
        }

        public void Dispose() => disposes.Dispose();
        
        private void Move(Vector2 moveVector) => playerView.PlayerMove.Move(moveVector);

        private void Shoot(bool performed) => playerView.PlayerShoot.Shoot(weaponController);
    }
}