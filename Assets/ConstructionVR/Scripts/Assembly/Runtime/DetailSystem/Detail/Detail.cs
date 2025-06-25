using System.IO;
using UnityEditor;
using UnityEngine;

namespace ConstructionVR.Assembly.Data
{
    [CreateAssetMenu(fileName = "Detail", menuName = "ConstructionVR/Assembly/Detail")]
    public class Detail : ScriptableObject
    {
        #region Fields

        [SerializeField]
        private string Name;

        [SerializeField]
        private bool isCheckAngle = true;

        [SerializeField]
        private AngleCheck angleCheckX;

        [SerializeField]
        private AngleCheck angleCheckY;

        [SerializeField]
        private AngleCheck angleCheckZ;

        #endregion

        #region Properteis

        public bool IsCheckAngle => isCheckAngle;

        public AngleCheck AngleCheckX => angleCheckX;

        public AngleCheck AngleCheckY => angleCheckY;

        public AngleCheck AngleCheckZ => angleCheckZ;

        #endregion


        public static Detail CreateBaseDetailSO(string nameDetail, string path = "Assets/ConstructionVR")
        {
#if UNITY_EDITOR
            if (!Directory.Exists(path))
            {
                AssetDatabase.CreateFolder("Assets", "ConstructionVR");
            }

            var detail = ScriptableObject.CreateInstance<Detail>();
            var localPath = path + $"/{nameDetail}.asset";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            AssetDatabase.CreateAsset(detail, localPath);
            AssetDatabase.SaveAssets();
            return detail;
#endif
            return null;
        }
    }
}
