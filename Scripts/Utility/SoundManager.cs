using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioMusic;
    public AudioSource audioMusicInGame;
    [Space]
    public AudioSource audioSource;

    public AudioClip _magnet;
    public AudioClip _trash;
    public AudioClip _Inventory;
    public AudioClip _Select;
    public AudioClip _Reward;

    public static SoundManager Ins { get { return instance; } }
    private static SoundManager instance;
    
    void Awake()
    {
        if (SoundManager.Ins != null && SoundManager.Ins != this)
        {
            Destroy(transform.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey("Music"))
            PlayerPrefs.SetInt("Music", 1);


        if (!PlayerPrefs.HasKey("Sound"))
            PlayerPrefs.SetInt("Sound", 1);

        SoundManager.Ins.PlayMusic(true);
    }

    public void PlaySound (AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
    }

    public void SetSound(bool bo)
    {
        if (bo)
            SoundManager.Ins.audioSource.volume = 1;
        else
            SoundManager.Ins.audioSource.volume = 0;
    }

    public void PlayMusic (bool play)
    {
        if (PlayerPrefs.GetInt("Music") == 0)
            return;
        if (play)
        {
            if (audioMusic.isPlaying)
                return;
            audioMusic.Play();
            audioMusicInGame.Stop();
        }
        else
        {
            audioMusic.Stop();
        }
    }

    public void PlayMusicInGame(bool play)
    {
        if (PlayerPrefs.GetInt("Music") == 0)
            return;
        if (play)
        {
            if (audioMusicInGame.isPlaying)
                return;
            audioMusicInGame.Play();
            audioMusic.Stop();
        }
        else
        {
            audioMusicInGame.Stop();
        }
    }
}
