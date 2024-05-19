using System.Collections.Generic;
using Infrastructure.StateMachines.Game;
using Infrastructure.StateMachines.Game.States;
using Interfaces;
using UI.Game.HUD;
using UI.Game.LoseWindow;
using UI.Game.WinWindow;
using UniRx;

namespace Controllers
{
    public interface IGameController : IDispose
    {
        void Play();
    }
    
    public class GameController : IGameController
    {
        private readonly GameStateMachine gameStateMachine;

        private readonly WinWindowController winWindowController;
        private readonly LoseWindowController loseWindowController;
        private readonly HUDController hudController;
        private readonly ITimeController timeController;

        private readonly CompositeDisposable disposes = new CompositeDisposable();
        private List<IDispose> gameDisposes;
        
        public GameController(GameStateMachine gameStateMachine, WinWindowController winWindowController, LoseWindowController loseWindowController, HUDController hudController, ITimeController timeController)
        {
            this.gameStateMachine = gameStateMachine;
            this.winWindowController = winWindowController;
            this.loseWindowController = loseWindowController;
            this.hudController = hudController;
            this.timeController = timeController;
        }

        public void Init(List<IDispose> gameDisposes)
        {
            this.gameDisposes = gameDisposes;
        }

        public void Dispose()
        {
            disposes.Dispose();
            foreach (var dispose in gameDisposes)
            {
                dispose.Dispose();
            }
        }

        public void Play()
        {
            timeController.Unpause();
        }
        
        private void Win()
        {
            gameStateMachine.Enter<WinState>();
            timeController.Pause();
            winWindowController.Show();
        }

        private void Lose()
        {
            timeController.Pause();
            gameStateMachine.Enter<LoseState>();
            loseWindowController.Show();
        }
    }
}