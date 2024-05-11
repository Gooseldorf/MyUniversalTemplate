using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Projectiles
{
    public class ProjectileReleaser : MonoBehaviour
    {
        [SerializeField] private Collider topCollider;
        [SerializeField] private Collider bottomCollider;

        private ProjectileViewBase catchedProjectile;
        
        public IObservable<ProjectileViewBase> ProjectileStream { get; private set;}

        private void Awake()
        {
            var topColliderStream = topCollider.OnTriggerEnterAsObservable()
                .Where(collision => collision.transform.parent.TryGetComponent(out catchedProjectile))
                .Select(_ => catchedProjectile);

            var bottomColliderStream = bottomCollider.OnTriggerEnterAsObservable()
                .Where(collision => collision.transform.parent.TryGetComponent(out catchedProjectile))
                .Select(_ => catchedProjectile);

            ProjectileStream = topColliderStream.Merge(bottomColliderStream);
        }
    }
}