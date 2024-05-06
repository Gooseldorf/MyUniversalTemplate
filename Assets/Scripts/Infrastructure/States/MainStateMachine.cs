using System;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Infrastructure.States
{
    public class MainStateMachine
    {
        private readonly Dictionary<Type, IState> states;
        private IState currentState;

        [Inject]
        public MainStateMachine(SceneLoader sceneLoader, IAssetProvider assetProvider)
        {
            states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader, new MenuFactory(assetProvider)),
                [typeof(MenuState)] = new MenuState(this),
                [typeof(LoadLevelState)] = new LoadLevelState(sceneLoader),
                [typeof(LevelState)] = new LevelState(),
                [typeof(QuitState)] = new QuitState()
            };
        }

        public void Enter<TState>() where TState : class, IStateNoArg
        {
            TState state = ChangeState<TState>();
            state.Enter();
            Debug.Log($"{typeof(TState)} entered");
        }

        public void Enter<TState, TArg>(TArg arg) where TState : class, IStateWithArg<TArg>
        {
            TState state = ChangeState<TState>();
            state.Enter(arg);
            Debug.Log($"{typeof(TState)} entered");
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