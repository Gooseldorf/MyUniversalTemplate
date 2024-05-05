using Zenject;

namespace Infrastructure.States
{
    public class LoadLevelState : IStateWithArg<string>
    {
        private readonly SceneLoader sceneLoader;

        public LoadLevelState(SceneLoader sceneLoader)
        {
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