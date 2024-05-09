using Infrastructure.Factories;
using UnityEngine;

namespace Infrastructure.StateMachines.Game.States
{
    public class LoadLevelState : IStateWithArg<int>
    {
        private readonly ILevelFactory levelFactory;
        
        public async void Enter(int levelIndex)
        {
            GameObject environment = await levelFactory.CreateEnvironment();
        }

        public void Exit()
        {
            
        }
    }
}