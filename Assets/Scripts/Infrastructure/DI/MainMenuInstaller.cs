using Infrastructure.Factories;
using Zenject;

namespace Infrastructure.DI
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMenuFactory>().To<MenuFactory>().AsSingle();
        }
    }
}