using Audio;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;

namespace Infrastructure.DI
{
    public class BootstrapInstaller : MonoInstallerBase
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            BindFactories();            
        }

        private void BindFactories()
        {
            Container.Bind<IAudioManagerFactory>().To<AudioManagerFactory>().AsSingle();
            Container.Bind<ILoadingScreenFactory>().To<LoadingScreenFactory>().AsSingle();
        }
    }
}