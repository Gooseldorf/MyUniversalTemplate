using Cysharp.Threading.Tasks;
using Infrastructure.Services;
using UI.Game;
using UI.Game.LoseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        UniTask<PlayerView> CreatePlayer();
        UniTask<GameObject> CreateEnvironment();
        UniTask<Canvas> CreateMainCanvas();
        UniTask<HUDView> CreateHUD(Canvas mainCanvas);
        UniTask<PauseWindowView> CreatePauseWindow(Canvas mainCanvas);
        UniTask<WinWindowView> CreateWinWindow(Canvas mainCanvas);
        UniTask<LoseWindowView> CreateLoseWindow(Canvas mainCanvas);
    }
}