using UnityEngine;

namespace ConstructionVR.Assembly.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        private static bool _isApplicationQuitting;

        public static T Instance
        {
            get
            {
                if (_isApplicationQuitting)
                    return null;

                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<T>();
                if (_instance != null)
                    return _instance;

                var obj = new GameObject { name = $"{typeof(T).Name}" };
                _instance = obj.AddComponent<T>();
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
                _instance = this as T;
            else
                Destroy(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            _isApplicationQuitting = true;
        }
    }

}
