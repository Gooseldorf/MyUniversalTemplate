using System;
using System.Collections.Generic;
using Audio;
using Infrastructure.StateMachines.Main.States;
using UI;

namespace Infrastructure.StateMachines.Main
{
    public class MainStateMachine : StateMachineBase
    {
        public MainStateMachine(SceneLoader sceneLoader, LoadingScreenController loadingScreenController, AudioManager audioManager)
        {
            States = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader, loadingScreenController, audioManager),
                [typeof(MenuState)] = new MenuState(),
                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader, loadingScreenController, audioManager),
                [typeof(GameState)] = new GameState(),
                [typeof(QuitState)] = new QuitState()
            };
        }
    }
}