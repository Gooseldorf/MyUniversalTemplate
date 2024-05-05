using UnityEngine;

namespace Infrastructure.States
{
    public class LoadMenuState: IStateNoArg
    {
        private const string MenuSceneName = "Menu";
        
        private readonly SceneLoader sceneLoader;
        
        public LoadMenuState(SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            sceneLoader.Load(MenuSceneName, OnLoad);
            Debug.Log($"{nameof(LoadMenuState)} Enter");
        }

        private void OnLoad()
        {
            Debug.Log($"{nameof(LoadMenuState)} OnLoad");
        }

        public void Exit()
        {
            Debug.Log($"{nameof(LoadMenuState)} Exit");
        }
    }
}