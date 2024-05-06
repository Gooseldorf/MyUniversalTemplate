using UnityEngine;

namespace Infrastructure.States
{
    public class QuitState: IStateNoArg
    {
        public void Enter()
        {
            CleanUp();
        }

        private void CleanUp()
        {
            Debug.Log("Cleaning up..");
            Exit();
        }

        public void Exit()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            UnityEngine.Application.Quit();
    #endif
        }
    }
}