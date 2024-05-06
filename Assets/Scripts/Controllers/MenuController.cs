using Infrastructure;
using Infrastructure.States;
using UI;
using UI.Menu;
using UniRx;
using UnityEngine;

namespace Controllers
{
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
            menuPanelView.StartButton.OnClickAsObservable.Subscribe(OnStartClick).AddTo(disposables);
            menuPanelView.SettingsButton.OnClickAsObservable.Subscribe(OnSettingsClick).AddTo(disposables);
            menuPanelView.ExitButton.OnClickAsObservable.Subscribe(OnExitClick).AddTo(disposables);
        }

        private void OnStartClick(Unit unit) => StartGame();

        private void StartGame()
        {
            mainStateMachine.Enter<LoadLevelState, string>(Constants.GAME_SCENE_NAME);
        }

        private void OnSettingsClick(Unit unit) => OpenSettings();

        private void OpenSettings()
        {
            Debug.Log("Settings");
        }

        private void OnExitClick(Unit unit) => Exit();

        private void Exit()
        {
            mainStateMachine.Enter<QuitState>();
        }
    }
}