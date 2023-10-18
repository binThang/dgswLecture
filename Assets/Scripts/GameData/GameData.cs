using System.Collections.Generic;

public class GameData
{
    //List<GameDataObject> gameDataList;
    GameDataObject[] gameDataList =
    {
        new PlayerData(),
        //new UserData(),
        //new InventoryData()
    };

    public void LoadAll()
    {
        //foreach (var gameDataObj in gameDataList)
        //{
        //    gameDataObj.LoadData();
        //}
    }

    public void SaveAll()
    {
        foreach (var gameDataObj in gameDataList)
        {
            if (gameDataObj.isDirty)
                gameDataObj.SaveData();
        }
    }
}
