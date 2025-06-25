using ConstructionVR.Assembly.Components;
using ConstructionVR.Assembly.GraphSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace ConstructionVR.Assembly.Editor
{
    public class СreatorAssemblyWindow : EditorWindow
    {
        #region StaticFields

        private static TemplateAssemblyMarker _templateMarker;

        #endregion

        #region Fields

        private GameObject _templateObject;

        UnityEditor.Editor gameObjectEditor;

        #endregion

        #region LifeCycle

        private void OnGUI()
        {
            if (_templateMarker == null)
            {
                DrawLinkTempleteObject();
                DrawInitializeButton();
                return;
            }

            EditorGUILayout.BeginHorizontal();

            ShowPreviewCurrentObject();

            EditorGUILayout.LabelField("Description");

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Update markers"))
            {
                СreatorAssembly.UpdateDetailsMarkerGroup(_templateMarker.gameObject, _templateMarker.AssemblyGraph);
            }

            if (GUILayout.Button("Create details"))
            {
                СreatorAssembly.GenerateDetails(_templateMarker.gameObject, _templateMarker.AssemblyGraph);
            }

            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Back"))
            {
                _templateMarker = null;
                gameObjectEditor = null;
                _templateObject = null;
            }
        }

        private void ShowPreviewCurrentObject()
        {
            GUIStyle bgColor = new GUIStyle();
            bgColor.normal.background = EditorGUIUtility.whiteTexture;

            if (gameObjectEditor == null)
                gameObjectEditor = UnityEditor.Editor.CreateEditor(_templateMarker.gameObject);

            gameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(125, 125), bgColor);
        }

        #endregion

        #region PublicMethods

        [MenuItem("Tools/СreatorAssembly")]
        public static void OpenWindow()
        {
            СreatorAssemblyWindow window = GetWindow<СreatorAssemblyWindow>();
            window.titleContent = new GUIContent("CreatorAssembly");
            window.minSize = new Vector2(200, 300);
            window.Show();
        }

        public static void OpenWindow(СreatorAssembly сreatorAssembly)
        {
            СreatorAssemblyWindow window = GetWindow<СreatorAssemblyWindow>();
            window.minSize = new Vector2(200, 300);
            window.Show();
        }

        #endregion

        #region PrivateMethods

        private void DrawLinkTempleteObject()
        {
            EditorGUILayout.LabelField("Select object for initialize");
            _templateObject = EditorGUILayout.ObjectField(_templateObject, typeof(GameObject), true) as GameObject;

            if (_templateObject == null)
                return;

            if (_templateObject.TryGetComponent<TemplateAssemblyMarker>(out var templateAssembly))
            {
                _templateMarker = templateAssembly;
            }
        }

        private void DrawInitializeButton()
        {
            if (_templateObject == null)
                return;

            if (GUILayout.Button("Initialize"))
            {
                InitializeTemplateObject();
            }
        }

        private void InitializeTemplateObject()
        {
            var folderPathInProject = EditorUtility.SaveFilePanelInProject("Folder save", "AssemblyObject", "", "");

            if (folderPathInProject.Equals(string.Empty))
                return;

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Directory.CreateDirectory(folderPathInProject);

            var pathToAssemblyTemplete = System.IO.Path.Combine(folderPathInProject, "AssemblyTemplate");
            var pathToAssemblyData = System.IO.Path.Combine(folderPathInProject, "AssemblyData");
            var pathToAssemblyBase = System.IO.Path.Combine(folderPathInProject, "AssemblyBase");
            var pathToDetails = System.IO.Path.Combine(folderPathInProject, "Details");
            var pathToConnectors = System.IO.Path.Combine(folderPathInProject, "Connectors");

            Directory.CreateDirectory(pathToAssemblyTemplete);
            Directory.CreateDirectory(pathToAssemblyData);
            Directory.CreateDirectory(pathToAssemblyBase);
            Directory.CreateDirectory(pathToDetails);
            Directory.CreateDirectory(pathToConnectors);

            var instanceRoot = Instantiate(_templateObject);
            var templatePrefab = PrefabUtility.SaveAsPrefabAsset(instanceRoot, pathToAssemblyTemplete + "/AssemblyTemplate.prefab");

            var graph = ScriptableObject.CreateInstance<AssemblyGraph>();
            graph.Initialize(pathToDetails, pathToAssemblyBase, pathToAssemblyData, pathToConnectors);
            AssetDatabase.CreateAsset(graph, (pathToAssemblyData + "/AssemblyGraph.asset"));

            _templateMarker = templatePrefab.AddComponent<TemplateAssemblyMarker>();
            _templateMarker.AssemblyGraph = graph;

            DestroyImmediate(instanceRoot);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            СreatorAssembly.AutoMarkingOfDetails(_templateMarker.gameObject, _templateMarker.AssemblyGraph);
        }

        #endregion
    }
}