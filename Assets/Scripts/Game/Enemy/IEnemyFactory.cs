﻿using Cysharp.Threading.Tasks;

namespace Game.Enemy
{
    public interface IEnemyFactory
    {
        UniTask WarmUp();
        EnemyView CreateEnemy();
    }
}