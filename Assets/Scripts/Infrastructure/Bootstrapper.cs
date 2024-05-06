using Infrastructure.AssetManagement;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class Bootstrapper: MonoBehaviour
    {
        private Main main;
        private IAssetProvider assetProvider;

        [Inject]
        public void Construct(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        private void Awake()
        {
            main = new Main(assetProvider);
            main.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}