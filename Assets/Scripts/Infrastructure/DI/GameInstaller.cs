using Audio;
using Controllers;
using Game.Environment;
using Game.Player;
using Infrastructure.AssetManagement;
using Infrastructure.Services.Input;
using UI.Game;

namespace Infrastructure.DI
{
    public class GameInstaller : MonoInstallerBase
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<ITimeController>().To<TimeController>().AsSingle();
            Container.Bind<IAudioManager>().To<AudioManager>().FromInstance(FindObjectOfType<AudioManager>());
            
            BindFactories();
        }

        private void BindFactories()
        {
            Container.Bind<IAudioManagerFactory>().To<AudioManagerFactory>().AsSingle();
            Container.Bind<IGameUIFactory>().To<GameUIFactory>().AsSingle();
            Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
        }
    }
}