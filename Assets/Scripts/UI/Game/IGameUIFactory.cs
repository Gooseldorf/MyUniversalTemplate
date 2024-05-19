using Controllers;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Input;
using Infrastructure.StateMachines.Game;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.PauseWindow;
using UI.Game.WinWindow;
using UnityEngine;

namespace UI.Game
{
    public interface IGameUIFactory
    {
        UniTask<Canvas> CreateWindowsCanvasAsync();
        UniTask<HUDController> CreateHUDAsync(HUDData HUDInitialData);
        UniTask<PauseWindowController> CreatePauseWindowAsync(Canvas windowsCanvas, GameStateMachine gameStateMachine, ITimeController timeController, IInputService inputService);
        UniTask<WinWindowController> CreateWinWindowAsync(Canvas windowsCanvas, GameStateMachine gameStateMachine);
        UniTask<LoseWindowController> CreateLoseWindowAsync(Canvas windowsCanvas, GameStateMachine gameStateMachine);
    }
}