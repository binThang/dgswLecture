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

    [SerializeField] private float bgmVolume = 0.5f;
    public float BGMVolume
    {
        get
        {
            return bgmVolume;
        }
        set
        {
            bgmVolume = value;
            bgmPlayer.volume = bgmVolume;
        }
    }

    [SerializeField] private float sfxVolume = 0.5f;
    public float SFXVolume
    {
        get
        {
            return sfxVolume;
        }
        set
        {
            sfxVolume = value;
        }
    }

    public enum SFX
    {
        Walk = 0,
        Attack,
        Hit,
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);

        _instance = this;
        Init();
    }

    private void Init()
    {
        bgmPlayer = bgmPlayerGO.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;

        sfxPlayer = new AudioSource[Channels];
        for (int i=0; i<Channels; i++)
        {
            sfxPlayer[i] = sfxPlayerGO.AddComponent<AudioSource>();
            sfxPlayer[i].playOnAwake = false;
        }
    }

    public void PlayBGM(bool on, int idx)
    {
        bgmPlayer.clip = bgmClips[idx];
        bgmPlayer.volume = bgmVolume;

        if (on)
            bgmPlayer.Play();
        else
            bgmPlayer.Stop();
    }

    private int channelIdx = 0;

    public void PlaySFX(SFX sfxType)
    {
        for (int i=0; i<Channels; i++)
        {
            int loopIdx = (channelIdx + i) % Channels;

            if (sfxPlayer[loopIdx].isPlaying)
                continue;

            channelIdx = loopIdx;
            sfxPlayer[loopIdx].clip = sfxClips[(int)sfxType];
            sfxPlayer[loopIdx].volume = sfxVolume;
            sfxPlayer[loopIdx].Play();
            break;
        }
    }
}
