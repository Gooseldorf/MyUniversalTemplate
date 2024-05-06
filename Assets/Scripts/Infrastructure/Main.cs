using Infrastructure.AssetManagement;
using Infrastructure.States;

namespace Infrastructure
{
    public class Main
    {
        public readonly MainStateMachine StateMachine;
        
        public Main(IAssetProvider assetProvider)
        {
            StateMachine = new MainStateMachine(new SceneLoader(), assetProvider);
        }
    }
}