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
    private RootInventoryfood inventoryfood;

    public void GetFoodRequest(UnityAction callback)
    {
        Debug.Log("GetFoodRequest");
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));
      //  client.Headers.Add("Content-Type", "application/json");

        client.Get(new Uri("https://www.pacheti.com/api/games/food/"), HttpCompletionOption.AllResponseContent, r =>
        {
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                string jsonData = "{\"inventoryfood\":" + r.ReadAsString() + "} ";
                inventoryfood = JsonUtility.FromJson<RootInventoryfood>(jsonData);
                player.player.inventoryfood = inventoryfood.inventoryfood;
                Debug.Log(jsonData);
                callback();
            }
        });

    }

}
