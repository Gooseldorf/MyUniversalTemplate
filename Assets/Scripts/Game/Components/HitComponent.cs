using System;
using UniRx;
using UnityEngine;

namespace Game.Components
{
    public class HitComponent : MonoBehaviour
    {
        public IObservable<Unit> HitStream => hitStreamSubject;
        private readonly Subject<Unit> hitStreamSubject = new Subject<Unit>();
        
        public void Hit() => hitStreamSubject.OnNext(new Unit());
    }
}