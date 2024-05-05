using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        public async void Load(string name, Action onSceneLoaded = null) => await LoadScene(name, onSceneLoaded);

        private async UniTask LoadScene(string name, Action onSceneLoaded)
        {
            //Check if already on <name> scene
            if (SceneManager.GetActiveScene().name == name)
            {
                onSceneLoaded?.Invoke();
                return;
            }

            await SceneManager.LoadSceneAsync(name).ToUniTask();
            onSceneLoaded?.Invoke();
        }
    }
}