using System;
using System.Collections.Generic;
using Infrastructure.StateMachines.Main.States;
using Managers;
using UI;

namespace Infrastructure.StateMachines.Main
{
    public class MainStateMachine : StateMachineBase
    {
        public MainStateMachine(SceneLoader sceneLoader, LoadingScreenController loadingScreenController, AudioManager audioManager)
        {
            States = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader, loadingScreenController, audioManager),
                [typeof(MenuState)] = new MenuState(this, audioManager),
                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader, loadingScreenController, audioManager),
                [typeof(GameState)] = new GameState(this),
                [typeof(QuitState)] = new QuitState()
            };
        }
    }
}