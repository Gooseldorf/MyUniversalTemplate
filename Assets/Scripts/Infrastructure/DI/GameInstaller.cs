using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Zenject;

namespace Infrastructure.DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }
    }
}