using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor.AddressableAssets;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

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
                var result = loadOp.Result;
                return result;
            }
            else
            {
                Debug.LogError($"Failed to load {address}.");
                return null;
            }
        }

        public async UniTask<T> LoadAddressable<T>(string address)
        {
            var loadOp = Addressables.LoadAssetAsync<T>(address);

            await UniTask.WaitUntil(() => loadOp.IsDone);
    
            if (loadOp.Result != null)
            {
                var result = loadOp.Result;
                return result;
            }
            else
            {
                Debug.LogError($"Failed to load {address}.");
                return default;
            }
        }

        public async UniTask<List<T>> LoadAddressableGroup<T>(string groupName)
        {
            List<T> loadedAssets = new List<T>();

            var group = AddressableAssetSettingsDefaultObject.Settings.FindGroup(g => g.name == groupName);

            if(group != null)
            {
                var tasks = new List<UniTask<T>>();

                foreach (var entry in group.entries)
                {
                    string key = entry.address;
                    tasks.Add(Addressables.LoadAssetAsync<T>(key).ToUniTask());
                }

                try 
                {
                    var results = await UniTask.WhenAll(tasks);
                    loadedAssets.AddRange(results);
                }
                catch (Exception e) 
                {
                    Debug.LogError($"Error loading addressables group with name {groupName}: {e}");
                }
            }
            else
            {
                Debug.Log($"Could not find group with name {groupName}");
            }

            return loadedAssets;
        }

        public void UnloadAddressables<T>(List<T> addressablesToUnload)
        {
            foreach (var addressable in addressablesToUnload)
            {
                Addressables.Release(addressable);
            }
            addressablesToUnload.Clear();
        }
    }
}