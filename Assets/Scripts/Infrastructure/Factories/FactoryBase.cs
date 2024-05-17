namespace Infrastructure.Factories
{
    public abstract class FactoryBase<T> where T : class
    {
        protected abstract T Create();
    }
}