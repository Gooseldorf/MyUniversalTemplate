using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using UnityEngine;

namespace Game.Player
{
    public interface IPlayerFactory
    {
        UniTask<PlayerController> CreatePlayerAsync(PlayerData playerData, IInputService inputService);
    }
    
    public class PlayerFactory : GameObjectFactoryBase, IPlayerFactory
    {
        public PlayerFactory(IAssetProvider assetProvider) : base(assetProvider) { }
        
        /// <summary>
        /// Create player and SetInitialState
        /// </summary>
        public async UniTask<PlayerController> CreatePlayerAsync(PlayerData playerData, IInputService inputService)
        {
            //Instantiate view
            GameObject player = await InstantiateAddressableAsync(playerData.PlayerAddress);
            player.TryGetComponent(out PlayerView playerView);
            
            //Create controller
            PlayerController playerController = new PlayerController(playerView, inputService);
            return playerController;
        }
    }
}