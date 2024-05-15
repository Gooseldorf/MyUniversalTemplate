using UnityEngine.Pool;

namespace Infrastructure.Pools
{
    public abstract class PoolBase <T> where T : class
    {
        public ObjectPool<T> Pool { get; private set; }

        protected PoolBase(int poolSize)
        {
            Pool = new ObjectPool<T>(Create, Get, Release, Destroy, true, poolSize);
        }

        protected abstract T Create();

        protected abstract void Get(T obj);
        
        protected abstract void Release(T obj);
        
        protected abstract void Destroy(T obj);
    }
}