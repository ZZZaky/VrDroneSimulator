using ConstructionVR.Assembly.Components;
using ConstructionVR.Assembly.Data;
using ConstructionVR.Assembly.GraphSystem;
using ConstructionVR.Assembly.Editor.Utilities;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ConstructionVR.Assembly
{
    public class СreatorAssembly : MonoBehaviour
    {
        #region PublicMethods

        public static void AutoMarkingOfDetails(GameObject assemblyTemplateObject, AssemblyGraph assemblyGraph)
        {
            var meshGroup = assemblyTemplateObject.GetComponentsInChildren<MeshFilter>(true);
            Dictionary<Mesh, DetailMeshGroup> meshDetailGroup = new Dictionary<Mesh, DetailMeshGroup>();

            foreach (var mesh in meshGroup)
            {
                if (meshDetailGroup.ContainsKey(mesh.sharedMesh))
                {
                    meshDetailGroup[mesh.sharedMesh].meshRenderers.Add(mesh);
                }
                else
                {
                    meshDetailGroup.Add(mesh.sharedMesh, new DetailMeshGroup(mesh));
                }
            }

            assemblyGraph.Clear();

            foreach (var delailsGroup in meshDetailGroup)
            {
                var connectorTypeNode = assemblyGraph.CreateConnectorType(delailsGroup.Key.name);
                connectorTypeNode.Initialize(assemblyGraph.PathToAssemblyData);

                foreach (var detail in delailsGroup.Value.meshRenderers)
                {
                    if (!detail.gameObject.TryGetComponent<DetailMarker>(out var marker))
                        marker = detail.gameObject.AddComponent<DetailMarker>();

                    marker.SetConnectorNode(connectorTypeNode);
                }
            }

            UpdateDetailsMarkerGroup(assemblyTemplateObject, assemblyGraph);
        }

        public static void UpdateDetailsMarkerGroup(GameObject assemblyTemplateObject, AssemblyGraph assemblyGraph)
        {
            var detailMarkers = assemblyTemplateObject.GetComponentsInChildren<DetailMarker>();
            Dictionary<ConnectorTypeNode, List<DetailMarker>> detailsMarkerGroup = new(detailMarkers.Length);

            foreach (var marker in detailMarkers)
            {
                if (marker.IsParentPart)
                    continue;

                if (marker.ConnectorType == null)
                {
                    marker.SetConnectorNode(assemblyGraph.CreateConnectorType(marker.name));
                }

                if (detailsMarkerGroup.ContainsKey(marker.ConnectorType))
                {
                    detailsMarkerGroup[marker.ConnectorType].Add(marker);
                }
                else
                {
                    detailsMarkerGroup.Add(marker.ConnectorType, new List<DetailMarker> { marker });
                }
            }

            assemblyGraph.Initialize(detailsMarkerGroup);
        }

        public static void GenerateDetails(GameObject assemblyTemplateObject, AssemblyGraph assemblyGraph)
        {
            string pathToSaveDetails = assemblyGraph.PathToDetails;

            if (pathToSaveDetails.Equals(""))
                return;

            //Делаем копию сборочного объекта, чтобы подогнать все детали в нужные позиции и пропорции.
            assemblyTemplateObject = CreateCopyObject(assemblyTemplateObject);

            //Создаем пустой объект, который является сборочной базой
            var assemblyBaseObject = CreateEmptyObjectWithParent(assemblyTemplateObject, "_AssemblyBase");

            //Создаем пустой объект, который является будущим контейнером для всех созданных деталей
            var detailsContainerObject = CreateEmptyObjectWithParent(assemblyTemplateObject, "_Details");

            //Генерируем слоты для подключения деталей (конекторы), для сборочной базы
            var connectors = CreatorUtility.GenerateDetailConnectors(assemblyTemplateObject, assemblyBaseObject);

            //Генерируем подключаемые детали
            var detailsGroup = CreatorUtility.GenerateConnectableDetails(assemblyTemplateObject, detailsContainerObject, out var connectorsInDetails);
            connectors.AddRange(connectorsInDetails);

            CreatePrefabsForConnectors(connectors, assemblyGraph.PathToConnectors);

            //Создаем префабы для деталей
            Dictionary<Detail, GameObject> detailPrefabs = new();

            foreach (var detailObject in detailsGroup)
            {
                var prefab = CreatorUtility.CreatePrefab(pathToSaveDetails, detailObject.Value[0]);

                if (!detailPrefabs.ContainsKey(detailObject.Key))
                {
                    detailPrefabs.Add(detailObject.Key, prefab);
                }
            }

            assemblyBaseObject.transform.parent = null;
            assemblyBaseObject.AddComponent<AssemblyBaseLogic>().SetAssemblyGraph(assemblyGraph);

            CreatorUtility.CreatePrefab(assemblyGraph.PathToAssemblyBase, assemblyBaseObject);
            AssetDatabase.SaveAssets();

            //Создаем детали из префабов в нужном количестве для сборки
            ReplaceDetailsOnPrefabs(detailsContainerObject, detailsGroup, detailPrefabs);

            detailsContainerObject.transform.parent = null;
            DestroyImmediate(assemblyTemplateObject);
        }

        private static void CreatePrefabsForConnectors(List<DetailConnector> connectors, string pathToSaveDetails)
        {
            Dictionary<ConnectorTypeNode, GameObject> connectorsPrefabsGroup = new();

            foreach (var connector in connectors)
            {
                if (!connectorsPrefabsGroup.ContainsKey(connector.ConnectorType))
                {
                    var connectorPrefab = CreatorUtility.CreatePrefab(pathToSaveDetails, connector.gameObject);
                    connectorsPrefabsGroup.Add(connector.ConnectorType, connectorPrefab);
                }

                var parentBuffer = connector.transform.parent;
                ReplaceObjectOnPrefab(connector.gameObject, connectorsPrefabsGroup[connector.ConnectorType])
                    .transform.parent = parentBuffer;
            }
        }

        private static void ReplaceDetailsOnPrefabs(GameObject rootObject, Dictionary<Detail, List<GameObject>> detailsGroup, Dictionary<Detail, GameObject> createblePrefabs)
        {
            foreach (var detailObject in detailsGroup)
            {
                int number = 0;
                var detailGroup = new GameObject(detailObject.Key.name);
                detailGroup.transform.parent = rootObject.transform;

                foreach (var det in detailObject.Value)
                {
                    var clonePrefabDetail = ReplaceObjectOnPrefab(det, createblePrefabs[detailObject.Key]);
                    clonePrefabDetail.name += number++;
                    clonePrefabDetail.transform.parent = detailGroup.transform;
                }
            }
        }

        private static GameObject ReplaceObjectOnPrefab(GameObject replaceObject, GameObject linkObjectPrefab)
        {
            var newObject = PrefabUtility.InstantiatePrefab(linkObjectPrefab, replaceObject.transform) as GameObject;
            newObject.transform.localPosition = Vector3.zero;
            newObject.transform.localRotation = Quaternion.identity;
            newObject.transform.localScale = Vector3.one;
            newObject.transform.parent = null;

            DestroyImmediate(replaceObject);
            return newObject;
        }

        private static GameObject CreateCopyObject(GameObject templateObject)
        {
            var nameBuffer = templateObject.name;
            templateObject = Instantiate(templateObject);
            templateObject.name = nameBuffer;

            return templateObject;
        }

        private static GameObject CreateEmptyObjectWithParent(GameObject parentObject, string nameSuffix)
        {
            var emptyObject = new GameObject(parentObject.name + nameSuffix);
            emptyObject.transform.SetParent(parentObject.transform);
            emptyObject.transform.localPosition = Vector3.zero;

            return emptyObject;
        }

        #endregion
    }
}