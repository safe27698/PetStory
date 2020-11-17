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
    public RootPlayer player;

    public void GetPlayerRequest(UnityAction callback)
    {
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));
      //  client.Headers.Add("Content-Type", "application/json");

        client.Get(new Uri("https://www.pacheti.com/api/games/player/"), HttpCompletionOption.AllResponseContent, r =>
        {
            Popup.Ins.PopupWaiting(false);
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                string jsonData = r.ReadAsString() ;
                player = JsonUtility.FromJson<RootPlayer>(jsonData);

                Debug.Log(jsonData);
                if(callback != null)
                    callback();
            }
        });

    }

}
