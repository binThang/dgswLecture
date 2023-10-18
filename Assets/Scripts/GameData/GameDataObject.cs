using System;
public abstract class GameDataObject
{
    public abstract string GetDataPath();
    public abstract void LoadData();
    public abstract void SaveData();
    public bool isDirty = false;
}
