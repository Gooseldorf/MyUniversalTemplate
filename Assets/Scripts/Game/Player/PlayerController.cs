using Data;
using Infrastructure.Services.Input;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerController : IInit, IDispose
    {
        void SetToInitialState();        
        void SetToState(PlayerData playerData);
        void SetPosition(Vector3 position);
    }
    
    public class PlayerController : IPlayerController
    {
        private readonly PlayerView playerView;
        private PlayerData initialPlayerData;
        private readonly IInputService inputService;
        
        private readonly CompositeDisposable disposes = new();
        
        public PlayerController(PlayerView playerView, IInputService inputService)
        {
            this.playerView = playerView;
            this.inputService = inputService;
        }

        public void Init()
        {
            //Sub to streams here
            //Example: inputService.Move2DStream.Subscribe(Move).AddTo(disposes);
        }

        public void Dispose() => disposes.Dispose();

        public void SetToInitialState() { }

        public void SetToState(PlayerData playerData)
        {
            initialPlayerData = playerData;
        }

        public void SetPosition(Vector3 position) => playerView.transform.position = position;
    }
}