using System.Collections.Generic;
using UnityEngine;

namespace WireVR.General
{
    public class GamePool<T> where T: MonoBehaviour
    {
        #region Fields

        private T _gameObjectPrefab;

        private Transform _container;

        private bool _autoExpand;

        private List<T> _pools = new List<T>();

        #endregion

        #region Constructors

        public GamePool(T prefab, bool autoExpand, int count)
        {
            _gameObjectPrefab = prefab;
            _autoExpand = autoExpand;
            _container = null;

            CreatePool(count);
        }
        
        public GamePool(T prefab, bool autoExpand, int count, Transform container)
        {
            _gameObjectPrefab = prefab;
            _autoExpand = autoExpand;
            _container = container;

            CreatePool(count);
        }

        #endregion

        #region Public Methods

        public bool TryGetFreeElement(out T element)
        {
            foreach (var pool in _pools)
            {
                if (!pool.gameObject.activeInHierarchy)
                {
                    element = pool;
                    pool.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (TryGetFreeElement(out var element))
                return element;

            if (_autoExpand)
                return CreateObject(true);
            
            Debug.LogError($"[GamePool]: There is no free elements in pool of type {typeof(T)}");
            return null;
        }

        #endregion

        #region Private Methods

        private void CreatePool(int count)
        {
            for (int i = 0; i <= count; i++)
                CreateObject();
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = UnityEngine.Object.Instantiate(_gameObjectPrefab, _container);

            createdObject.gameObject.SetActive(isActiveByDefault);
            _pools.Add(createdObject);

            return createdObject;
        }

        #endregion
    }
}
