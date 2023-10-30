using UnityEngine;
using System;
using System.Collections;

public class StaticManager : MonoBehaviour
{
    private static StaticManager _instance = null;
    public static StaticManager Instance => _instance;

    public static GameData GameData;
    //public static Chart Chart;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        StartCoroutine(Save());
    }

    IEnumerator Save()
    {
        while(true)
        {
            yield return new WaitForSeconds(60);

            GameData.SaveAll();
        }
    }
}
