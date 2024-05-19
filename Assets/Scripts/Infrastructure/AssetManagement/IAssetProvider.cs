using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        UniTask<GameObject> InstantiateAddressableAsync(string address);
        UniTask<List<T>> LoadAddressableLabel<T>(string labelName);
#if UNITY_EDITOR
        UniTask<List<T>> LoadAddressableGroup<T>(string groupName);
#endif
        void UnloadAddressables<T>(List<T> addressablesToUnload);
        UniTask<T> LoadAddressable<T>(string address);
    }
}