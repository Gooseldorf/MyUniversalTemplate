using UnityEngine;

namespace Infrastructure
{
    public class Bootstraper: MonoBehaviour, ICoroutineRunner
    {
        private Game game;

        private void Awake()
        {
            game = new Game(this);
            game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}