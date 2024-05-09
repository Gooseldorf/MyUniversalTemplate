using Controllers;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services.Input;

namespace Infrastructure.DI
{
    public class GameInstaller : MonoInstallerBase
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<ITimeController>().To<TimeController>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<IGameUIFactory>().To<GameUIFactory>().AsSingle();
        }
    }
}