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
        UniTask<PlayerController> CreatePlayer(PlayerData playerData, IInputService inputService);
    }
    
    public class PlayerFactory : GameObjectFactoryBase, IPlayerFactory
    {
        public PlayerFactory(IAssetProvider assetProvider) : base(assetProvider) { }
        
        public async UniTask<PlayerController> CreatePlayer(PlayerData playerData, IInputService inputService)
        {
            GameObject player =  await InstantiateAddressableAsync(playerData.PlayerAddress);
            player.TryGetComponent(out PlayerView playerView);
            PlayerController playerController = new PlayerController(playerView, inputService);
            playerController.SetPosition(playerData.PlayerStartPosition);
            playerController.SetSpeed(playerData.PlayerSpeed);
            return playerController;
        }
    }
}