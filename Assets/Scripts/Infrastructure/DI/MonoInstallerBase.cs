using Zenject;

namespace Infrastructure.DI
{
    public abstract class MonoInstallerBase : MonoInstaller
    {
        public TBinding Resolve<TBinding>() => Container.Resolve<TBinding>();

        public void BindAsSingle<IT, T>() where T : IT => Container.Bind<IT>().To<T>().AsSingle();

        public void BindAsSingleFromInstance<IT, T>(T instance) where T : IT => Container.Bind<IT>().To<T>().FromInstance(instance).AsSingle();
    }
}