using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services;
using Infrastructure.Services.Input;

namespace Infrastructure.States
{
    public class BootstrapState : IStateNoArg
    {
        private const string BootstrapSceneName = "Initial";
        
        private readonly GameStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;
        private AllServices container;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            container = AllServices.Container;
            RegisterServices();
            sceneLoader.Load(BootstrapSceneName, EnterLoadLevel);
        }
        public void Exit()
        {
            
        }
        
        private void RegisterServices()
        {
            container.RegisterSingle(new InputService());
            container.RegisterSingle(new AssetProvider());
            container.RegisterSingle(new GameFactory(container.Single<AssetProvider>()));
            
            //TODO: Register input service
        }
        
        private void EnterLoadLevel() => stateMachine.Enter<LoadLevelState, string>("Main");
    }
}