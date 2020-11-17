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
    private RootInventorycloth inventorycloths;

    public void GetClothRequest(UnityAction callback)
    {
        Debug.Log("GetClothRequest");
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));
      //  client.Headers.Add("Content-Type", "application/json");

        client.Get(new Uri("https://www.pacheti.com/api/games/cloth/"), HttpCompletionOption.AllResponseContent, r =>
        {
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                string jsonData = "{\"inventorycloths\":" + r.ReadAsString() + "} ";
                inventorycloths = JsonUtility.FromJson<RootInventorycloth>(jsonData);
                player.player.inventorycloth = inventorycloths.inventorycloths;
                Debug.Log(jsonData);
                callback();
            }
        });

    }

}
