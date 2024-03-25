
namespace Infrastructure
{
    public class LoadLevelState : IStateWithArg<string>
    {
        private readonly GameStateMachine stateMachine;
        private readonly SceneLoader sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            this.stateMachine = stateMachine;
            this.sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) => sceneLoader.Load(sceneName, OnLoad);

        private void OnLoad()
        {
            //Instantiate player prefab
            //Instantiate HUD
            //Instantiate all other stuff like camera etc
        }

        public void Exit()
        {
            
        }
    }
}