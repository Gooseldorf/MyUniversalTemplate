using UnityEditor;

namespace Infrastructure.StateMachines.Main.States
{
    public class QuitState: IStateNoArg
    {
        public void Enter()
        {
            CleanUp();
        }

        private void CleanUp()
        {
            Exit();
        }

        public void Exit()
        {
    #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
    #else
            UnityEngine.Application.Quit();
    #endif
        }
    }
}