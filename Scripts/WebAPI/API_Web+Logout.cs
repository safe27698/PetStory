using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CI.HttpClient;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public partial class API_Web : MonoBehaviour
{
    [Space]
    [SerializeField]
    private bool startLogout;
    [SerializeField]
    private ResponseLogout m_Logout;

    public void LogoutWebRequest()
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));

        client.Get(new Uri("https://www.pacheti.com/api/user/logout/"), HttpCompletionOption.AllResponseContent, r =>
        {
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                Debug.Log(r.ReadAsString());
                m_Logout = JsonUtility.FromJson<ResponseLogout>(r.ReadAsString());

                PlayerPrefs.DeleteKey("token");
                PlayerPrefs.DeleteKey("slug");
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("Music", 1);
                PlayerPrefs.SetInt("Sound", 1);
                Popup.Ins.PopupWaiting(false);
            }
        });
        
    }


    [System.Serializable]
    public class ResponseLogout
    {
        public string result;
        public string msg;
    }

}
