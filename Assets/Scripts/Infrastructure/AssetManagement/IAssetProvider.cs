using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        UniTask<GameObject> InstantiateAddressable(string address);

        UniTask<AudioClip> LoadAudioAddressable(string address);
        UniTask<List<T>> LoadAddressableGroup<T>(string groupName);
        void UnloadAddressables<T>(List<T> addressablesToUnload);
    }
}