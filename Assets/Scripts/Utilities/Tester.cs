#if UNITY_EDITOR

using NaughtyAttributes;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [Button]
    private void CheckTester()
    {
        Debug.Log("Tester checked");
    }
}

#endif
