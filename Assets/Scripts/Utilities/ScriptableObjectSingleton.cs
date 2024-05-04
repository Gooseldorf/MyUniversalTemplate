using UnityEngine;

namespace Utilities
{
    public class ScriptableObjectSingleton<T>: ScriptableObject where T : ScriptableObject
    {
        private static T instance;
        
        public static bool IsLoaded => instance != null;

        public static T Instance
        {
            get
            {
                if (instance)
                    return instance;

                return  instance = Resources.Load<T>(typeof(T).Name);
            }
        }
    }
}