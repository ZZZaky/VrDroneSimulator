using System;

namespace FDS.UI.Views
{
    class TypeSetupView<T> : View
    {
        #region Events

        public event Action<T> OnValueApplyed;

        #endregion

        #region Protected Methods

        protected void SetupValue(T newValue)
        {
            OnValueApplyed?.Invoke(newValue);
        }

        #endregion
    }
}
