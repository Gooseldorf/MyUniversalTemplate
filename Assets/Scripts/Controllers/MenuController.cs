using Infrastructure;
using Infrastructure.StateMachines.Main;
using Infrastructure.StateMachines.Main.States;
using Interfaces;
using UI.Menu;
using UniRx;
using UnityEngine;

namespace Controllers
{
    public interface IMenuController : IInit, IDispose { }
    
    public class MenuController : IMenuController
    {
        private readonly MenuPanelView menuPanelView;
        private readonly MainStateMachine mainStateMachine;
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public MenuController(MainStateMachine mainStateMachine, MenuPanelView menuPanelView)
        {
            this.menuPanelView = menuPanelView;
            this.mainStateMachine = mainStateMachine;
        }

        public void Init()
        {
            SubscribeToClicks();
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
        
        private void SubscribeToClicks()
        {
            menuPanelView.StartButton.OnClickStream.Subscribe(OnStartClick).AddTo(disposables);
            menuPanelView.SettingsButton.OnClickStream.Subscribe(OnSettingsClick).AddTo(disposables);
            menuPanelView.ExitButton.OnClickStream.Subscribe(OnExitClick).AddTo(disposables);
        }

        private void OnStartClick(Unit unit) => StartGame();

        private void StartGame() => mainStateMachine.Enter<LoadGameState, string>(Constants.GAME_SCENE_NAME);

        private void OnSettingsClick(Unit unit) => OpenSettings();

        private void OpenSettings()
        {
            Debug.Log("Settings");
        }

        private void OnExitClick(Unit unit) => Exit();

        private void Exit() => mainStateMachine.Enter<QuitState>();
    }
}