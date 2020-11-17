using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public enum Tutorials
    {
        t1,t2,t3,t4
    }
    public Tutorials tutorials;
    public SettingData settingData;

    // Start is called before the first frame update
    void OnEnable()
    {
        switch (tutorials)
        {
            case Tutorials.t1:
                if(PlayerPrefs.GetString(tutorials.ToString()) == "f")
                    gameObject.SetActive(false);
                break;
            case Tutorials.t2:
                if (PlayerPrefs.GetString(tutorials.ToString()) == "f")
                    gameObject.SetActive(false);
                break;
            case Tutorials.t3:
                if (PlayerPrefs.GetString(tutorials.ToString()) == "f")
                    gameObject.SetActive(false);
                break;
            case Tutorials.t4:
                if (PlayerPrefs.GetString(tutorials.ToString()) == "f")
                    gameObject.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSet(Toggle toggle)
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetString(tutorials.ToString(), "f");
            //switch (tutorials)
            //{
            //    case Tutorials.t1:
            //        PlayerPrefs.SetString(tutorials.ToString(),"f");
            //        break;
            //    case Tutorials.t2:
            //        settingData.tutorial_2 = true;
            //        break;
            //    case Tutorials.t3:
            //        settingData.tutorial_3 = true;
            //        break;
            //    case Tutorials.t4:
            //        settingData.tutorial_4 = true;
            //        break;
            //}
        }
        else
        {
            PlayerPrefs.SetString(tutorials.ToString(), "t");
            //switch (tutorials)
            //{
            //    case Tutorials.t1:
            //        settingData.tutorial_1 = false;
            //        break;
            //    case Tutorials.t2:
            //        settingData.tutorial_2 = false;
            //        break;
            //    case Tutorials.t3:
            //        settingData.tutorial_3 = false;
            //        break;
            //    case Tutorials.t4:
            //        settingData.tutorial_4 = false;
            //        break;
            //}
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
