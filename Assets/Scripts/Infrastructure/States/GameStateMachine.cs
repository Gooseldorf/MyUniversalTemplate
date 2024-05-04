using System;
using System.Collections.Generic;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> states;
        private IState currentState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            //Setup dictionary with all game states 
            states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader)
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