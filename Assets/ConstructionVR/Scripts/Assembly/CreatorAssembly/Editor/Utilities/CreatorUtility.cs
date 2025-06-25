using ConstructionVR.Assembly.Components;
using ConstructionVR.Assembly.Data;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ConstructionVR.Assembly.Editor.Utilities
{
    public static class CreatorUtility
    {
        #region PublicMethods

        public static GameObject CreatePrefab(string path, GameObject gameObject)
        {
            Debug.Log(gameObject);
            string localPath = System.IO.Path.Combine(path, gameObject.name + ".prefab");
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            bool prefabSuccess;
            var prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath,
                InteractionMode.UserAction, out prefabSuccess);

            if (prefabSuccess == true)
                Debug.Log("Prefab was saved successfully");
            else
                Debug.Log("Prefab failed to save" + prefabSuccess);

            return prefab;
        }

        public static GameObject CreateMarkerObject(DetailMarker marker, string sf)
        {
            var markerObject = new GameObject(marker.name + "_" + sf);

            markerObject.transform.SetParent(marker.transform);
            TransformLocalReset(markerObject.transform);
            markerObject.transform.parent = null;

            var connectorMeshFilter = markerObject.AddComponent<MeshFilter>();
            var connectorMeshRender = markerObject.AddComponent<MeshRenderer>();

            if (marker.gameObject.TryGetComponent<MeshFilter>(out var meshFilter))
            {
                EditorUtility.CopySerialized(meshFilter, connectorMeshFilter);
            }

            if (marker.gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer))
            {
                EditorUtility.CopySerialized(meshRenderer, connectorMeshRender);
            }

            return markerObject;
        }

        public static DetailConnector CreateConnector(DetailMarker marker)
        {
            var connectorObject = CreateMarkerObject(marker, "Connector");

            var connector = connectorObject.AddComponent<DetailConnector>();
            connector.Initialize(marker.ConnectorType);

            return connector;
        }

        public static ConnectableDetail CreateConnectableDetail(DetailMarker marker, out List<DetailConnector> connectorsInDetail)
        {
            var connectableObject = CreateMarkerObject(marker, "Connectable");

            connectorsInDetail = GenerateDetailConnectors(marker.gameObject, connectableObject);
            GeneratePartDetail(marker.gameObject, connectableObject);

            var connectable = connectableObject.AddComponent<ConnectableDetail>();
            connectable.Initialize(marker.ConnectorType.GetFirsConnectableDetail());

            return connectable;
        }

        public static void GeneratePartDetail(GameObject rootDetail, GameObject parentDetail)
        {
            for (int i = 0; i < rootDetail.transform.childCount; i++)
            {
                if (rootDetail.transform.GetChild(i).TryGetComponent<DetailMarker>(out var marker))
                {
                    if (!marker.IsParentPart)
                        continue;

                    var clonePart = CreateMarkerObject(marker, "Part");
                    clonePart.transform.SetParent(parentDetail.transform);
                }
            }
        }

        public static List<DetailConnector> GenerateDetailConnectors(GameObject rootDetail, GameObject parentDetail)
        {
            var connectors = new List<DetailConnector>();

            for (int i = 0; i < rootDetail.transform.childCount; i++)
            {
                if (rootDetail.transform.GetChild(i).TryGetComponent<DetailMarker>(out var marker))
                {
                    if (marker.IsParentPart)
                        continue;

                    var connector = CreateConnector(marker);
                    connector.transform.SetParent(parentDetail.transform);

                    connectors.Add(connector);
                }
            }

            return connectors;
        }

        public static Dictionary<Detail, List<GameObject>> GenerateConnectableDetails(GameObject rootDetail, GameObject parentDetail,
            out List<DetailConnector> connectorsInDetails)
        {
            var markers = rootDetail.GetComponentsInChildren<DetailMarker>();
            Dictionary<Detail, List<GameObject>> detailsDic = new();

            connectorsInDetails = new List<DetailConnector>();

            for (int i = 0; i < markers.Length; i++)
            {
                if (markers[i].IsParentPart)
                    continue;

                var detail = CreateConnectableDetail(markers[i], out var connectors);
                connectorsInDetails.AddRange(connectors);

                detail.transform.parent = parentDetail.transform;

                if (detailsDic.ContainsKey(detail.TypeDetail))
                {
                    detailsDic[detail.TypeDetail].Add(detail.gameObject);
                }
                else
                {
                    detailsDic.Add(detail.TypeDetail, new List<GameObject> { detail.gameObject });
                }
            }

            return detailsDic;

        }

        public static Detail CreateBaseDetailSO(string nameDetail, string path = "Assets/ConstructionVR")
        {
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
        }

        public static void TransformLocalReset(Transform arg)
        {
            arg.localPosition = Vector3.zero;
            arg.localRotation = Quaternion.identity;
            arg.transform.localScale = Vector3.one;
        }

        #endregion
    }
}
