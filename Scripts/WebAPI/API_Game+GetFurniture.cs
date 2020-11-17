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

    public void GetFurnitureRequest(UnityAction callback)
    {
        Debug.Log("GetFurnitureRequest");
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));
      //  client.Headers.Add("Content-Type", "application/json");

        client.Get(new Uri("https://www.pacheti.com/api/games/furniture/"), HttpCompletionOption.AllResponseContent, r =>
        {
            Popup.Ins.PopupWaiting(false);
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                Debug.Log(r.ReadAsString());
                callback();
            }
        });

    }

}
