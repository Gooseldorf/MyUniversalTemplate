using Cysharp.Threading.Tasks;
using Data;
using Game.Player;
using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProvider assetProvider;

        public PlayerFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        public async UniTask<PlayerView> CreatePlayer(LevelData levelData)
        {
            GameObject player =  await assetProvider.InstantiateAddressable(levelData.PlayerAddress);
            player.transform.position = levelData.PlayerPosition;
            player.TryGetComponent(out PlayerView playerView);
            playerView.PlayerMove.SetSpeed(levelData.PlayerSpeed);
            return playerView;
        }
    }

    public interface IPlayerFactory
    {
        UniTask<PlayerView> CreatePlayer(LevelData levelData);
    }
}