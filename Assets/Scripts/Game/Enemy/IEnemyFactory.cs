using Cysharp.Threading.Tasks;
using Data;

namespace Game.Enemy
{
    public interface IEnemyFactory
    {
        EnemyData EnemyData { set; }
        UniTask WarmUpIfNeeded();
        EnemyView CreateEnemy();
    }
}