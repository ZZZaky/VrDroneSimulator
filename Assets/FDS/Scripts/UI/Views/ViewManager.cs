using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FDS.UI.Views
{
    class ViewManager : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private List<View> views;

        [SerializeField]
        private View defaultView;

        private View _currentView;
        private bool _initialized = false;

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            if (_initialized)
                return;
            _initialized = true;
            foreach(var view in views)
            {
                view.InitializeView(null);
                view.Hide(null);
            }
            SelectView(defaultView);
        }

        #endregion

        #region Public Methods

        public void SelectView(View view)
        {
            _currentView?.Hide(null);
            _currentView = view;
            _currentView.Show(null);
        }

        #endregion
    }
}
