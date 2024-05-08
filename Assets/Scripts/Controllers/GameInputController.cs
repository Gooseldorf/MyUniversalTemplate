using Infrastructure.Services.Input;
using Interfaces;
using UniRx;

namespace Controllers
{
    public class GameInputController : IInit, IDispose
    {
        private readonly IInputService inputService;
        private readonly TimeController timeController;
        private PlayerController playerController;
        
        public GameInputController(IInputService inputService, TimeController timeController, PlayerController playerController)
        {
            this.inputService = inputService;
            this.timeController = timeController;
            this.playerController = playerController;
        }
        
        public void Init()
        {
            inputService.PauseStream.Subscribe(Pause);
        }

        public void Dispose()
        {
            
        }

        private void Pause(bool performed) => timeController.Pause();
    }
}