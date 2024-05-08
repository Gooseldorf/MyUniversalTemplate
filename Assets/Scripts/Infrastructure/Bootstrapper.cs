using Infrastructure.States;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper: MonoBehaviour
    {
        private Main main;
        
        private void Awake()
        {
            main = new Main();
            main.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}