using Cysharp.Threading.Tasks;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameUIFactory
    {
        UniTask<Canvas> CreateMainCanvas();
        UniTask<HUDView> CreateHUD(Canvas mainCanvas);
        UniTask<PauseWindowView> CreatePauseWindow(Canvas mainCanvas);
        UniTask<WinWindowView> CreateWinWindow(Canvas mainCanvas);
        UniTask<LoseWindowView> CreateLoseWindow(Canvas mainCanvas);
    }
}