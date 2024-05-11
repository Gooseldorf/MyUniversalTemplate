using Infrastructure;
using Infrastructure.Factories;

namespace Game.Enemy
{
    public class EnemyPool : PoolBase<EnemyView>
    {
        public EnemyPool(FactoryBase factory, int poolSize) : base(factory, poolSize)
        {
        }

        protected override EnemyView Create()
        {
            EnemyView obj = ((EnemyFactory)Factory).CreateEnemy();
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}