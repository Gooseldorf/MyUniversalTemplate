using Cysharp.Threading.Tasks;
using Data;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Game.Player
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProvider assetProvider;

        public PlayerFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        public async UniTask<PlayerView> CreatePlayer(PlayerData playerData)
        {
            GameObject player =  await assetProvider.InstantiateAddressable(playerData.PlayerAddress);
            player.TryGetComponent(out PlayerView playerView);
            playerView.transform.position = playerData.PlayerStartPosition;
            playerView.PlayerMove.SetSpeed(playerData.PlayerSpeed);
            return playerView;
        }
    }

    public interface IPlayerFactory
    {
        UniTask<PlayerView> CreatePlayer(PlayerData playerData);
    }
}