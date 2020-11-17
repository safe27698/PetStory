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
    private bool startEditProfile;
    [SerializeField]
    private string firstName;
    [SerializeField]
    private string lastName;

    void EditProfileWebRequest()
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));
        
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        keyValuePairs.Add("first_name_th", firstName);
        keyValuePairs.Add("last_name_th", lastName);

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);

        client.Patch(new Uri("https://www.pacheti.com/api/user/" + m_Login.slug + "/edit/"), content, HttpCompletionOption.AllResponseContent, r =>
        {
            Popup.Ins.PopupWaiting(false);
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                Debug.Log(r.ReadAsString());
            }
        });

    }
    
}
