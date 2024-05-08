using Cysharp.Threading.Tasks;
using UI;

namespace Infrastructure.Factories
{
    public interface IBootstrapFactory
    {
        UniTask<LoadingScreenView> CreateLoadingScreen();
    }
}