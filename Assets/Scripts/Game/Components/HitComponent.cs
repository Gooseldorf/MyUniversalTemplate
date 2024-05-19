using System;
using UniRx;
using UnityEngine;

namespace Game.Components
{
    [RequireComponent(typeof(Collider))]
    public class HitComponent : MonoBehaviour
    {
        [SerializeField] private Collider collider;
        
        public IObservable<bool> HitStream => hitStreamSubject;
        
        private readonly Subject<bool> hitStreamSubject = new Subject<bool>();

        public void Hit(bool isPlayerProjectile) => hitStreamSubject.OnNext(isPlayerProjectile);
    }
}