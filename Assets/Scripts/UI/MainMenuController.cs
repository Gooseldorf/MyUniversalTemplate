using Infrastructure.States;

public class MainMenuController
{
    private readonly GameStateMachine stateMachine;
    
    public MainMenuController(GameStateMachine gameStateMachine)
    {
        stateMachine = gameStateMachine;
    }

    private void OnStartClick()
    {
        
    }

    private void OnSettingsClick()
    {
        
    }

    private void OnExitClick()
    {
        
    }
}
