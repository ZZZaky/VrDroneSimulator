using System.Collections.Generic;
using UnityEngine;

namespace FDS.UI.Views
{
    public class View : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private List<View> children;

        #endregion

        #region Public Methods

        public virtual void InitializeView(object arg)
        {
            foreach (var child in children)
                child.InitializeView(null);
        }

        public virtual void Show(object arg)
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide(object arg)
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
