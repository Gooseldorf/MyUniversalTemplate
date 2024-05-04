using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Controls controls;
    private Controls.GameActions gameActions;

    private void Awake()
    {
        controls = new Controls();
        controls.Enable();
        gameActions = controls.Game;
        Subscribe();
    }

    private void OnDestroy()
    {
        controls.Disable();
        controls.Dispose();
        Unsubscribe();
    }

    private void Test(InputAction.CallbackContext context) => Debug.Log("InputTest");
    
    void Update()
    {
        
    }

    private void Subscribe()
    {
        gameActions.TEST.performed += Test;
    }

    private void Unsubscribe()
    {
        gameActions.TEST.performed -= Test;
    }
}
