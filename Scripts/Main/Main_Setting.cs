using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Setting : MonoBehaviour
{
    public GameObject setting;
    public GameObject musicSet;
    public GameObject soundSet;

    public bool working;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
            musicSet.SetActive(true);
        else
            musicSet.SetActive(false);


        if (PlayerPrefs.GetInt("Sound") == 0)
            soundSet.SetActive(true);
        else
            soundSet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenClose()
    {
        setting.SetActive(!setting.gameObject.activeInHierarchy);
        working = setting.gameObject.activeInHierarchy;
    }

    public void SetMusic(GameObject g)
    {
        if (!g.active)
        {
            SoundManager.Ins.PlayMusic(false);
            SoundManager.Ins.PlayMusicInGame(false);
            PlayerPrefs.SetInt("Music", 0);
            g.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
            SoundManager.Ins.PlayMusicInGame(true);
            g.SetActive(false);
        }
    }


    public void SetSound(GameObject g)
    {
        if (!g.active)
        {
            SoundManager.Ins.SetSound(false);
            PlayerPrefs.SetInt("Sound", 0);
            g.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            SoundManager.Ins.SetSound(true);
            g.SetActive(false);
        }
    }
}
