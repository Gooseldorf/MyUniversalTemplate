namespace Infrastructure.States
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
            sceneLoader.Load(BootstrapSceneName, EnterLoadLevel);
        }
        public void Exit()
        {
            
        }
        
        private void EnterLoadLevel() => stateMachine.Enter<LoadLevelState, string>("Main");
    }
}