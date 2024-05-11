using System;
using Interfaces;

namespace Game.Enemy
{
    public class EnemyController : IDispose
    {
        private readonly EnemyView enemyView;

        public event Action<EnemyController, EnemyView> Dead;

        public EnemyController(EnemyView enemyView)
        {
            this.enemyView = enemyView;
        }

        public void Init()
        {
        }

        public void Dispose()
        {
            
        }
    }
}