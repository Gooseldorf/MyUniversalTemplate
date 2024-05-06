using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        } 
        
        public GameObject Instantiate(string path, Vector3 position)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }

        public async UniTask<GameObject> InstantiateAddressable(string address)
        {
            var loadOp = Addressables.InstantiateAsync(address);

            await UniTask.WaitUntil(() => loadOp.IsDone);
    
            if (loadOp.Result != null)
            {
                var menu = loadOp.Result;
                return menu;
            }
            else
            {
                Debug.LogError($"Failed to load {address}.");
                return null;
            }
        }
    }
}