using Zenject;

namespace Infrastructure.DI
{
    public abstract class MonoInstallerBase : MonoInstaller
    {
        public TBinding Resolve<TBinding>()
        {
            return Container.Resolve<TBinding>();
        }
    }
}