using Interfaces;

namespace Controllers
{
    public interface IGameController : IDispose
    {
        void Play();
    }
}