using System;
using System.IO;
using System.Linq;
using Tools.Timer;
using UnityEngine;

public class FlyResultsContainer : MonoBehaviour
{

    #region Fields

    [SerializeField] private MapRecordsSO mapRecordsSO;

    #endregion

    #region Events

    public event Action OnValueChanged;

    #endregion

    #region LifeCycle

    private void Start()
    {
        string filePath = Application.dataPath + "/records.json";
        if (File.Exists(filePath))
        {
            DataWrapper<RecordData> records = JsonUtility.FromJson<DataWrapper<RecordData>>(File.ReadAllText(filePath));
            mapRecordsSO.records = records.FlyData.ToList();
        }
    }

    private void OnApplicationQuit()
    {
        string filePath = Application.dataPath + "/records.json";

        var dataWrapperClass = new DataWrapper<RecordData>();
        dataWrapperClass.FlyData = mapRecordsSO.records.ToArray();
        string json = JsonUtility.ToJson(dataWrapperClass);

        File.WriteAllText(filePath, json);

        mapRecordsSO.records.Clear();
    }

    #endregion

    #region PublicMethods

    public void AddRecord(int sceneId, string time)
    {
        bool isFound = false;
        for(int i = 0; i < mapRecordsSO.records.Count; i++)
        {
            if (mapRecordsSO.records[i].id == sceneId)
            {
                isFound = true;
                TimerTime previousTime = new TimerTime(mapRecordsSO.records[i].result);
                TimerTime currentTime = new TimerTime(time);
                if(previousTime > currentTime)
                {
                    mapRecordsSO.records[i] = new RecordData(sceneId, time);
                }
            }
        }
        if (!isFound)
        {
            mapRecordsSO.records.Add(new RecordData(sceneId, time));
        }
        OnValueChanged?.Invoke();
    }

    public string GetMapRecord(int sceneId)
    {
        for (int i = 0; i < mapRecordsSO.records.Count; i++)
        {
            if (mapRecordsSO.records[i].id == sceneId)
            {
                return mapRecordsSO.records[i].result;
            }
        }
        return "--/--/--";
    }

    #endregion

    #region NestedTypes

    [Serializable]
    private class DataWrapper<T>
    {
        public T[] FlyData;
    }

    #endregion
}
