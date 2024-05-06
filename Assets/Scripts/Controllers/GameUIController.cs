using Interfaces;

namespace Controllers
{
    public class GameUIController : IGameUIController 
    {
        public void Init()
        {
        
        }

        public void Dispose()
        {
        
        }
    }

    public interface IGameUIController : IInit, IDispose
    {
    }
}