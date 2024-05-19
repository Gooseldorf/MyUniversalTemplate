using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Updater : MonoBehaviour
{
    private List<IUpdate> updatables = new List<IUpdate>();

    void Update()
    {
        foreach (var updatable in updatables)
        {
            updatable.Update();
        }
    }

    public void AddUpdatable(IUpdate updatable) => updatables.Add(updatable);

    public void RemoveUpdatable(IUpdate updatable) => updatables.Remove(updatable);
}
