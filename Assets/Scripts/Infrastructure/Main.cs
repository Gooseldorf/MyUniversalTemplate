using Infrastructure.AssetManagement;
using Infrastructure.Services.Input;
using Infrastructure.States;
using Zenject;

namespace Infrastructure
{
    public class Main
    {
        public readonly MainStateMachine StateMachine;
        
        [Inject]
        public Main(IAssetProvider assetProvider, IInputService input)
        {
            StateMachine = new MainStateMachine(new SceneLoader(), assetProvider, input);
        }
    }
}