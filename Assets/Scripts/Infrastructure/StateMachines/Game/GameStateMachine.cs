using System;
using System.Collections.Generic;
using Infrastructure.StateMachines.Game.States;
using Infrastructure.StateMachines.Main;

namespace Infrastructure.StateMachines.Game
{
    public class GameStateMachine : StateMachineBase
    {
        public int CurrentLevelIndex { get; private set; }
        public int NextLevelIndex { get; private set; }
        
        public GameStateMachine(MainStateMachine mainStateMachine)
        {
            CurrentLevelIndex = 0;
            NextLevelIndex = 0;
            States = new Dictionary<Type, IState>()
            {
                [typeof(LoadLevelState)] = new LoadLevelState(this),
                [typeof(StartState)] = new StartState(this, mainStateMachine),
                [typeof(LevelState)] = new LevelState(this),
                [typeof(WinState)] = new WinState(this),
                [typeof(LoseState)] = new LoseState(this),
                [typeof(QuitToMenuState)] = new QuitToMenuState(mainStateMachine)
            };
        }
    }
}