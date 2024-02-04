using UnityEngine;
using UnityEngine.Pool;

namespace Utilities
{
    public class SimpleGenericPool<T>: MonoBehaviour where T: Component
    {
        [SerializeField] private protected T prefab;
        [SerializeField] private protected int poolSize;

        public ObjectPool<T> ObjPool { get; private set; }


        private void Awake()
        {
            if (prefab == null)
            {
                Debug.LogError($"{name}: No prefab!");
                return;
            }
            ObjPool = new ObjectPool<T>(Create, Get, Release, Destroy,true, poolSize);
        }

        private protected virtual T Create()
        {
            T obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            return obj;
        }

        private protected virtual void Get(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        private protected virtual void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.position = transform.position;
        }

        private protected virtual void Destroy(T obj)
        {
            GameObject.Destroy(obj);
        }
    }
}