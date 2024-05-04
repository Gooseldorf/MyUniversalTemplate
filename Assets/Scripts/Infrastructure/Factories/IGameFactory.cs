using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(GameObject at);
        void InstantiateHUD();
    }
}