using System;
using System.Collections.Generic;

namespace Infrastructure.StateMachines
{
    public class StateMachineBase
    {
        protected Dictionary<Type, IState> States;
        private IState currentState;
        
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
        
        private TState GetState<TState>() where TState : class, IState => States[typeof(TState)] as TState;
    }
}