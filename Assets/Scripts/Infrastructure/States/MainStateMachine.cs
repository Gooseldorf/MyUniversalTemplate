using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services.Input;

namespace Infrastructure.States
{
    public class MainStateMachine
    {
        private readonly Dictionary<Type, IState> states;
        private IState currentState;
        
        public MainStateMachine(SceneLoader sceneLoader)
        {
            states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader),
                [typeof(MenuState)] = new MenuState(this),
                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader),
                [typeof(GameState)] = new GameState(),
                [typeof(QuitState)] = new QuitState()
            };
        }

        public void Enter<TState>() where TState : class, IStateNoArg
        {
            //Debug.Log($"{typeof(TState)}");
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TArg>(TArg arg) where TState : class, IStateWithArg<TArg>
        {
            //Debug.Log($"{typeof(TState)}");
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