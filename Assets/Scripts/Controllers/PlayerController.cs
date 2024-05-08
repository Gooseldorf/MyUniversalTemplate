using Infrastructure.Services.Input;

namespace Controllers
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