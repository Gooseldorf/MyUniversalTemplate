using Infrastructure;
using Infrastructure.Factories;

namespace Game.Projectiles
{
    public class ProjectilePool : PoolBase<ProjectileViewBase>
    {
        private readonly ProjectileReleaser releaser;

        public ProjectilePool(FactoryBase factory, int poolSize) : base(factory, poolSize)
        {
            
        }

        public void Init()
        {
            
        }

        protected override ProjectileViewBase Create()
        {
            throw new System.NotImplementedException();
        }
    }
}