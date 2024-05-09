using Infrastructure.StateMachines.MainStateMachine;

namespace Infrastructure.States
{
    public class BootstrapState : IStateNoArg
    {
        private readonly MainStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;
        
        public BootstrapState(MainStateMachine stateMachine, SceneLoader sceneLoader)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            sceneLoader.Load(Constants.BOOTSTRAP_SCENE_NAME, OnLoad);
        }

        private void OnLoad()
        {
            EnterLoadMenu();
        }

        public void Exit()
        {
            
        }

        private void EnterLoadMenu() => stateMachine.Enter<LoadMenuState>();
    }
}