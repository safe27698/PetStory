using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CI.HttpClient;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using UnityEngine.Events;

public partial class API_Game : MonoBehaviour
{
    
    public void PostClothRequest(int clothId, int type, UnityAction callback)
    {
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        keyValuePairs.Add("clothid", clothId.ToString());
        keyValuePairs.Add("type", type.ToString());

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);
        
        client.Post(new Uri("https://www.pacheti.com/api/games/cloth/"), content, HttpCompletionOption.AllResponseContent, r =>
        {
            Popup.Ins.PopupWaiting(false);
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                Popup.Ins.PopupWaiting(true);
                Debug.Log(r.ReadAsString());
                GetPlayerRequest(callback);
              //  callback();
            }
        });

    }

}
