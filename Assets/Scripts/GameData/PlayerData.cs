[System.Serializable]
public class PlayerData : GameDataObject
{
    public string NickName;
    public int Level;
    public float Exp;
    public string[] Items;

    public override string GetDataPath()
    {
        throw new System.NotImplementedException();
    }

    public override void LoadData()
    {
        throw new System.NotImplementedException();
    }

    public override void SaveData()
    {
        throw new System.NotImplementedException();
    }
}
