using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using UI.Game;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        UniTask<PlayerView> CreatePlayer();
        UniTask<GameObject> CreateEnvironment();
        UniTask<Canvas> CreateMainCanvas();
        UniTask<HUDView> CreateHUD();
        UniTask<PauseWindowView> CreatePauseWindow();
    }
}