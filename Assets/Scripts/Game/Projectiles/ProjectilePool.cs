using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.Pools;

namespace Game.Projectiles
{
    public class ProjectilePool : ComponentPoolBase<ProjectileViewBase>
    {
        private readonly ProjectileReleaser releaser;

        public ProjectilePool(GameObjectFactoryBase factory, int poolSize) : base(factory, poolSize)
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