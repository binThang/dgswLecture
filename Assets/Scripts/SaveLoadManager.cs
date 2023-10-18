using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    public PlayerData playerData;

    [SerializeField] TMPro.TextMeshProUGUI textNickname;
    [SerializeField] TMPro.TextMeshProUGUI textLevel;
    [SerializeField] TMPro.TextMeshProUGUI textExp;

    // Start is called before the first frame update
    void Start()
    {
        textNickname.text = playerData.NickName;
        textLevel.text = "LV." + playerData.Level;
        textExp.text = "EXP : " + playerData.Exp;
    }

    public void Save()
    {
        // Make Json
        string json = JsonUtility.ToJson(playerData, true);

        // Set Data Path
        string path = Path.Combine(Application.persistentDataPath, "playerData.json");
        Debug.Log(path);

        // File Write
        File.WriteAllText(path, json);

        // Using Player Prefs
        //PlayerPrefs.SetString("Nickname", playerData.NickName);
        //PlayerPrefs.SetInt("Level", playerData.Level);
        //PlayerPrefs.SetFloat("Exp", playerData.Exp);
    }

    public void Load()
    {
        // Data Path
        string path = Path.Combine(Application.dataPath, "playerData.json");

        // Get Json
        string json = File.ReadAllText(path);

        // Make Instance
        playerData = JsonUtility.FromJson<PlayerData>(json);

        // Using Player Prefs
        //playerData.NickName = PlayerPrefs.GetString("Nickname");
        //playerData.Level = PlayerPrefs.GetInt("Level");
        //playerData.Exp = PlayerPrefs.GetFloat("Exp");

        // Update UI
        textNickname.text = playerData.NickName;
        textLevel.text = "LV." + playerData.Level;
        textExp.text = "EXP : " + playerData.Exp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
