using Infrastructure.States;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstraper: MonoBehaviour
    {
        private Game game;

        private void Awake()
        {
            game = new Game();
            game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}