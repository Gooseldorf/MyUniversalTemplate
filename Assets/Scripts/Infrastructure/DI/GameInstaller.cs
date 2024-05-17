using Audio;
using Controllers;
using Game.Enemy;
using Game.Environment;
using Game.Player;
using Game.Weapon;
using Infrastructure.AssetManagement;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using UI.Game;
using UnityEngine;

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
            Container.Bind<IGameUIFactory>().To<GameUIFactory>().AsSingle();
            Container.Bind<ILevelFactory>().To<LevelFactory>().AsSingle();
            Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
        }

        public void BindAsSingle<IT, T>() where T : IT => Container.Bind<IT>().To<T>().AsSingle();

        public void BindAsSingleFromInstance<IT, T>(T instance) where T : IT => Container.Bind<IT>().To<T>().FromInstance(instance).AsSingle();
    }
}