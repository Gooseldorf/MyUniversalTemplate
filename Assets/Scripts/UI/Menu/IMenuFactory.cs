using Cysharp.Threading.Tasks;

namespace UI.Menu
{
    public interface IMenuFactory
    {
        UniTask<MenuPanelView> CreateMenu();
    }
}