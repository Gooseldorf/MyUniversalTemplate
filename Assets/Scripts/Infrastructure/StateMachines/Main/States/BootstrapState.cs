namespace Infrastructure.StateMachines.Main.States
{
    /// <summary>
    /// Initiates loading of a first game scene
    /// </summary>
    public class BootstrapState : IStateWithArg<bool>
    {
        private readonly MainStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;
        
        public BootstrapState(MainStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public void Enter(bool loadMainMenu)
        {
            if(loadMainMenu)
                stateMachine.Enter<LoadMenuState>();
            else
                stateMachine.Enter<LoadGameState, string>(Constants.GAME_SCENE_NAME);
        }

        public void Exit() { }
    }
}