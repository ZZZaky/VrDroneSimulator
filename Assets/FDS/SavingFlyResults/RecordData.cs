
[System.Serializable]
public struct RecordData
{
    public int id;
    public string result;

    public RecordData(int id, string result)
    {
        this.id = id;
        this.result = result;
    }

}
