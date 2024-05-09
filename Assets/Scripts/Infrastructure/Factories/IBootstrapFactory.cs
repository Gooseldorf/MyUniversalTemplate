using Cysharp.Threading.Tasks;
using Managers;
using UI;

namespace Infrastructure.Factories
{
    public interface IBootstrapFactory
    {
        UniTask<LoadingScreenView> CreateLoadingScreen();
        UniTask<AudioManager> CreateAudioManager();
    }
}