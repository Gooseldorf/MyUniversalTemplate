using Infrastructure.Services.Input;

namespace Game.Player
{
    public class PlayerController
    {
        private readonly IInputService inputService;

        public PlayerController(IInputService inputService)
        {
            this.inputService = inputService;
        }
    }
}