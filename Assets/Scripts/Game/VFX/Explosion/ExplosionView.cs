using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game.VFX.Explosion
{
    public class ExplosionView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionParticles;
        public IObservable<ExplosionView> CompleteStream { get; private set; }

        private Subject<ExplosionView> onCompleteSubject;

        public void Init()
        {
            onCompleteSubject = new Subject<ExplosionView>();
            CompleteStream = onCompleteSubject.AsObservable();
        }

        public void Play()
        {
            explosionParticles.Play(true);
            WaitForParticleFinish().Forget();
        }

        public void ResetParticles()
        {
            explosionParticles.Stop(true);
            explosionParticles.Clear(true);
        }

        private async UniTaskVoid WaitForParticleFinish()
        {
            await UniTask.Delay((int)(explosionParticles.main.duration * 1000));
            onCompleteSubject.OnNext(this);
        }
    }
}