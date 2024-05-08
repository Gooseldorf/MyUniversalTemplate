using Cysharp.Threading.Tasks;
using UI.Menu;

namespace Infrastructure.Factories
{
    public interface IMenuFactory
    {
        UniTask<MenuPanelView> CreateMenu();
    }
}