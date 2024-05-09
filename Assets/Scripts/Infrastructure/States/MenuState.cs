﻿using Controllers;
using Managers;
using UI;

namespace Infrastructure.States
{
    public class MenuState : IStateWithArg<IMenuController>
    {
        private readonly MainStateMachine stateMachine;
        private IMenuController menuController;

        public MenuState(MainStateMachine mainStateMachine, AudioManager audioManager)
        {
            stateMachine = mainStateMachine;
        }

        public void Enter(IMenuController menuController)
        {
            this.menuController = menuController;
            menuController.Init();
        }

        public void Exit()
        {
            menuController.Dispose();
        }
    }
}