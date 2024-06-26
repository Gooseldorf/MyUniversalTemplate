using System;
using Interfaces;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CityView : MonoBehaviour, IInit
{
    [SerializeField] private Collider cityCollider;
    
    public IObservable<Collider> CityBreachedStream;

    public void Init()
    {
        CityBreachedStream = cityCollider.OnTriggerEnterAsObservable().Select(collider => collider);
    }
}
