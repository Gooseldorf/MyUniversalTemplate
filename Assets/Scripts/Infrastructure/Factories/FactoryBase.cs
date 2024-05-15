namespace Infrastructure.Factories
{
    public abstract class FactoryBase<T> where T : class
    {
        public abstract T Create();
    }
}