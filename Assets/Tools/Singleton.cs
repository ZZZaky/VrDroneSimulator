using UnityEngine;

namespace Tools
{
    public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        #region Fields

        private static T _instance;

        #endregion

        #region Properties

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }

        #endregion
    }
}
