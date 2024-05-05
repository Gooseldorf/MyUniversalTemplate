#if UNITY_EDITOR

using Infrastructure.Services.Input;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class Tester : MonoBehaviour
{
    private IInputService service;

    [Inject]
    public void Construct(IInputService inputService)
    {
        service = inputService;
    }
    
    [Button]
    private void CheckTester()
    {
        Debug.Log("Tester checked");
        if(service == null)
            Debug.Log("service is null");
        else
        {
            Debug.Log("Has service");
        }
    }
}

#endif
