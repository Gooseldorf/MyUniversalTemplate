using Infrastructure.Services.Input;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public class PlayerController : IPlayerController
    {
        private readonly PlayerView playerView;
        private readonly IInputService inputService;
        private readonly CompositeDisposable disposes = new();

        public PlayerController(PlayerView playerView, IInputService inputService)
        {
            this.playerView = playerView;
            this.inputService = inputService;
        }

        public void Init()
        {
            inputService.Move2DStream.Subscribe(Move).AddTo(disposes);
        }

        public void Dispose() => disposes.Dispose();
        
        private void Move(Vector2 moveVector) => playerView.PlayerMove.Move(moveVector);
    }

    public interface IPlayerController : IInit, IDispose
    {
    }
}