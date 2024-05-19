using Data;
using Infrastructure.Services.Input;
using Interfaces;
using UniRx;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerController : IInit, IDispose
    {
        void SetToInitialState(PlayerData playerData);
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

        /// <summary>
        /// Subscribe to playerView and inputService streams
        /// </summary>
        public void Init()
        {
            //Example: inputService.Move2DStream.Subscribe(Move).AddTo(disposes);
        }

        /// <summary>
        /// Unsub from playerView and inputService streams 
        /// </summary>
        public void Dispose() => disposes.Dispose();

        /// <summary>
        /// Sets playerController to data state.
        /// </summary>
        public void SetToInitialState(PlayerData playerData)
        {
            initialPlayerData = playerData;
        }

        public void SetPosition(Vector3 position) => playerView.transform.position = position;
    }
}