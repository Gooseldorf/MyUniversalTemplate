using Managers;

namespace Infrastructure.States
{
    public class GameState: IStateWithArg<string>
    {
        private readonly AudioManager audioManager;

        public GameState(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        public void Enter(string arg)
        {
            
        }

        public void Exit()
        {
            
        }
    }
}