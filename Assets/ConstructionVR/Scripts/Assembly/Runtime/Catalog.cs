using ConstructionVR.Assembly.Utilities;
using UnityEngine;

namespace ConstructionVR.Assembly
{
    public abstract class Catalog : Singleton<Catalog>
    {
        #region PublicMethods

        public abstract void Register<T>(T regrObjct) where T : Transform;

        public abstract void Unregister<T>(T unregObject) where T : Transform;

        #endregion
    }
}