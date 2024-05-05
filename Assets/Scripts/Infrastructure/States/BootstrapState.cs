namespace Infrastructure.States
{
    public class BootstrapState : IStateNoArg
    {
        private const string BootstrapSceneName = "Bootstrap";
        
        private readonly GameStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            sceneLoader.Load(BootstrapSceneName, EnterLoadMenu);
        }
        public void Exit()
        {
            
        }

        private void EnterLoadMenu() => stateMachine.Enter<LoadMenuState>();
    }
}