using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using UI;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    /// <summary>
    /// Creates Menu UI Elements
    /// </summary>
    public class MenuFactory : IMenuFactory
    {
        private readonly IAssetProvider assetProvider;

        [Inject]
        public MenuFactory(IAssetProvider assets)
        {
            assetProvider = assets;
        }

        public async UniTask<MenuPanelView> CreateMenu()
        {
            GameObject menu =  await assetProvider.InstantiateAddressable("Menu");
            menu.TryGetComponent(out MenuPanelView menuPanelView);
            return menuPanelView;
        }
    }
}