using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyRecordsData", menuName = "ScriptableObjects/Records", order = 1)]
public class MapRecordsSO : ScriptableObject
{
    public List<RecordData> records;
}
