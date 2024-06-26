﻿using Infrastructure.Factories;
using UnityEngine;
using UnityEngine.Pool;

namespace Infrastructure.Pools
{
    public abstract class ComponentPoolBase <T> where T : Component
    {
        protected readonly CachedGameObjectFactoryBase Factory;
        protected readonly GameObject Container;

        public ObjectPool<T> Pool { get; private set; }

        protected ComponentPoolBase(CachedGameObjectFactoryBase factory, int poolSize)
        {
            Factory = factory;
            Pool = new ObjectPool<T>(Create, Get, Release, Destroy, true, poolSize);
            Container = new GameObject(GetType().Name + "Container");
        }

        protected abstract T Create();

        protected virtual void Get(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void Destroy(T obj)
        {
            Object.Destroy(obj.gameObject);
        }
    }
}