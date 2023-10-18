using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;

    [SerializeField] AudioClip[] bgmClips;
    [SerializeField] AudioClip[] sfxClips;
    [SerializeField] int Channels;

    [SerializeField] GameObject bgmPlayerGO;
    [SerializeField] GameObject sfxPlayerGO;

    AudioSource bgmPlayer;
    AudioSource[] sfxPlayer;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        _instance = this;
        Init();
    }

    private void Init()
    {

    }
}
