using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using UI;

namespace Infrastructure.DI
{
    public class MenuInstaller : MonoInstallerBase
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IMenuFactory>().To<MenuFactory>().AsSingle();
        }
    }
}