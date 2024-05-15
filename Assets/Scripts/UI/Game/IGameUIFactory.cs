using Cysharp.Threading.Tasks;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace UI.Game
{
    public interface IGameUIFactory
    {
        UniTask<Canvas> CreateWindowsCanvas();
        UniTask<HUDView> CreateHUD();
        UniTask<PauseWindowView> CreatePauseWindow(Canvas windowsCanvas);
        UniTask<WinWindowView> CreateWinWindow(Canvas windowsCanvas);
        UniTask<LoseWindowView> CreateLoseWindow(Canvas windowsCanvas);
    }
}