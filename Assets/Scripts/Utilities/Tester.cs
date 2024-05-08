#if UNITY_EDITOR

using Infrastructure.Services.Input;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class Tester : MonoBehaviour
{
    private IInputService input;
    
    [Inject]
    public void Construct(IInputService input)
    {
        this.input = input;
    }

    [Button]
    private void CheckInput()
    {
        Debug.Log(input.Move2DStream);
    }
    
    [Button]
    private void CheckTester()
    {
        Debug.Log("Tester checked");
    }
}

#endif
