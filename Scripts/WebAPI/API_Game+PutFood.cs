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
    private int foodId;

    public void PutFoodRequest(int foodId, int count, UnityAction callback)
    {
        HttpClient client = new HttpClient();

        foreach (Inventoryfood f in player.player.inventoryfood)
        {
            if (f.foodid == foodId)
                foodId = f.id;
        }

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        keyValuePairs.Add("count",count.ToString());

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);

        client.Put(new Uri("https://www.pacheti.com/api/games/food/"+ foodId.ToString()+"/"), content, HttpCompletionOption.AllResponseContent, r =>
        {
            Popup.Ins.PopupWaiting(false);
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                Debug.Log(r.ReadAsString());
                GetPlayerRequest(callback);
            }
        });

    }

}
