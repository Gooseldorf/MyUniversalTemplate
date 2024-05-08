using System;
using System.Collections.Generic;
using UI;

namespace Infrastructure.States
{
    public class MainStateMachine
    {
        private readonly Dictionary<Type, IState> states;
        private IState currentState;
        
        public MainStateMachine(SceneLoader sceneLoader, LoadingScreenController loadingScreenController)
        {
            states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader, loadingScreenController),
                [typeof(MenuState)] = new MenuState(this),
                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader, loadingScreenController),
                [typeof(GameState)] = new GameState(),
                [typeof(QuitState)] = new QuitState()
            };
        }

        public void Enter<TState>() where TState : class, IStateNoArg
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TArg>(TArg arg) where TState : class, IStateWithArg<TArg>
        {
            TState state = ChangeState<TState>();
            state.Enter(arg);
        }
        
        private TState ChangeState<TState>() where TState : class, IState
        {
            currentState?.Exit(); 
            
            TState state = GetState<TState>();
            currentState = state;
            return state;
        }
        
        private TState GetState<TState>() where TState : class, IState => states[typeof(TState)] as TState;
    }
}