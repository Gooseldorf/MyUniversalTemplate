using Data;
using Interfaces;

namespace Controllers
{
    public interface IGameController : IDispose
    {
        void Play(LevelData levelData);
    }
}