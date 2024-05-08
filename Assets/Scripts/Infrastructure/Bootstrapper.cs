using Infrastructure.AssetManagement;
using Infrastructure.Services.Input;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrapper: MonoBehaviour
    {
        private Main main;
        private IAssetProvider assetProvider;
        private IInputService inputService;

        [Inject]
        public void Construct(IAssetProvider assetProvider, IInputService inputService)
        {
            this.assetProvider = assetProvider;
            this.inputService = inputService;
        }
        
        private void Awake()
        {
            main = new Main(assetProvider, inputService);
            main.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}