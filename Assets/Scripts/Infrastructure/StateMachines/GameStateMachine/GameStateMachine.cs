using System;
using System.Collections.Generic;
using Infrastructure.StateMachines.GameStateMachine.States;
using Infrastructure.States;

namespace Infrastructure.StateMachines.GameStateMachine
{
    public class GameStateMachine : StateMachineBase
    {
        public int CurrentLevelIndex { get; private set; }
        public int NextLevelIndex { get; private set; }
        
        public GameStateMachine()
        {
            States = new Dictionary<Type, IState>()
            {
                [typeof(LoadLevelState)] = new LoadLevelState(),
                [typeof(StartState)] = new StartState(),
                [typeof(LevelState)] = new LevelState(),
                [typeof(WinState)] = new WinState(),
                [typeof(LoseState)] = new LoseState(),
            };
        }
    }
}