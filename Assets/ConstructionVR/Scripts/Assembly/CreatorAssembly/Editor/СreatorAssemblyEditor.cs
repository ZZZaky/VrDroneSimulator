using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace ConstructionVR.Assembly.Editor
{
    [CustomEditor(typeof(СreatorAssembly))]
    public class СreatorAssemblyEditor : UnityEditor.Editor
    {
        #region Fields

        private SerializedProperty _graphProperty;
        private SerializedProperty _detailMeshGroupProperty;
        private SerializedProperty _baseDetail;

        private bool _isShowList = false;

        #endregion

        #region LifeCycle

        private void OnEnable()
        {
            _graphProperty = serializedObject.FindProperty("assemblyGraph");
            _detailMeshGroupProperty = serializedObject.FindProperty("detailMeshGroup");
            _baseDetail = serializedObject.FindProperty("baseDetail");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var labelStyle = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 15 };

            EditorGUILayout.LabelField("Settings graph", labelStyle);
            EditorGUILayout.Space(10);

            var assemblyLogic = (СreatorAssembly)target;

            EditorGUILayout.PropertyField(_graphProperty);
            var graphObject = _graphProperty.objectReferenceValue;

            if (graphObject != null)
            {
                if (GUILayout.Button("Edit", GUILayout.Height(30)))
                    СreatorAssemblyWindow.OpenWindow(assemblyLogic);

                //if (GUILayout.Button("AutoMarkingOfDetails", GUILayout.Height(30)))
                //    assemblyLogic.AutoMarkingOfDetails(null,null);

                if (GUILayout.Button("Edit graph", GUILayout.Height(30)))
                    NodeEditorWindow.Open(graphObject as XNode.NodeGraph);

                EditorGUILayout.Space(20);
                EditorGUILayout.LabelField("Settings details", labelStyle);

                EditorGUILayout.Space(10);

                EditorGUILayout.PropertyField(_baseDetail);
                _isShowList = EditorGUILayout.BeginFoldoutHeaderGroup(_isShowList, "Details group");

                //if (_isShowList)
                //{
                //    foreach (var detailsGroup in assemblyLogic.DetailsMarkerGroup)
                //    {
                //        EditorGUILayout.LabelField($"Group {detailsGroup.Key.name}");
                //        foreach (var detail in detailsGroup.Value)
                //        {
                //            EditorGUILayout.ObjectField(detail.gameObject, typeof(GameObject), true);
                //        }
                //    }
                //}

                EditorGUILayout.EndFoldoutHeaderGroup();

                //if (GUILayout.Button("UpdateDetailsMarkerGroup", GUILayout.Height(30)))
                //    assemblyLogic.UpdateDetailsMarkerGroup(null);

                //if (GUILayout.Button("Generate details", GUILayout.Height(30)))
                //    assemblyLogic.GenerateDetails();
            }
            else
            {
                if (GUILayout.Button("Create graph", GUILayout.Height(30)))
                {
                    //TODO
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
