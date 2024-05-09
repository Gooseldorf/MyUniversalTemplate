using Cysharp.Threading.Tasks;
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
        
        public async UniTask<PlayerView> CreatePlayer()
        {
            GameObject player =  await assetProvider.InstantiateAddressable("Player");
            player.TryGetComponent(out PlayerView playerView);
            return playerView;
        }
    }

    public interface IPlayerFactory
    {
        UniTask<PlayerView> CreatePlayer();
    }
}