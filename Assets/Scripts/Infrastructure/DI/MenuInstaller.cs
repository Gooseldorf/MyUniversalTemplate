using Infrastructure.AssetManagement;
using UI.Menu;

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