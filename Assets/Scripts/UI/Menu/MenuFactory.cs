using Controllers;
using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.StateMachines.Main;
using UnityEngine;

namespace UI.Menu
{
    public interface IMenuFactory
    {
        UniTask<MenuController> CreateMenu(MainStateMachine mainStateMachine);
    }
    
    public class MenuFactory : GameObjectFactoryBase, IMenuFactory
    {
        public MenuFactory(IAssetProvider assetProvider) : base(assetProvider) { }

        public async UniTask<MenuController> CreateMenu(MainStateMachine mainStateMachine)
        {
            GameObject menu =  await InstantiateAddressableAsync("Menu");
            menu.TryGetComponent(out MenuPanelView menuPanelView);
            MenuController menuController = new MenuController(mainStateMachine, menuPanelView);
            return menuController;
        }
    }
}