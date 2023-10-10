using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI titleText;
    [SerializeField] Button startButton;
    [SerializeField] Button loadButton;
    [SerializeField] Button creditButton;

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = "Practice Code";

        startButton.onClick.AddListener(OnClickStart);
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
