namespace Infrastructure
{
    public class BootstrapState : IStateNoArg
    {
        private const string BootstrapSceneName = "Initial";
        
        private readonly GameStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            RegisterServices();
            sceneLoader.Load(BootstrapSceneName);
        }

        private void EnterLoadLevel() => stateMachine.Enter<LoadLevelState, string>("Main");

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            //TODO: Register input service
        }
        
    }
}