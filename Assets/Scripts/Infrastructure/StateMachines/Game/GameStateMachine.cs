using System;
using System.Collections.Generic;
using Infrastructure.StateMachines.Game.States;

namespace Infrastructure.StateMachines.Game
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
                [typeof(QuitToMenuState)] = new QuitToMenuState()
            };
        }
    }
}