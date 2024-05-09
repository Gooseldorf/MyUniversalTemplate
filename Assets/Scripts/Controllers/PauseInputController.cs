using Infrastructure.Services.Input;
using Interfaces;
using UniRx;

namespace Controllers
{
    public class PauseInputController : IInit, IDispose
    {
        private readonly IInputService inputService;
        private readonly TimeController timeController;

        private readonly CompositeDisposable disposes = new();
        
        public PauseInputController(IInputService inputService, TimeController timeController)
        {
            this.inputService = inputService;
            this.timeController = timeController;
        }
        
        public void Init() => inputService.PauseStream.Subscribe(Pause).AddTo(disposes);

        public void Dispose() => disposes.Dispose();

        private void Pause(bool performed) => timeController.Pause();
    }
}