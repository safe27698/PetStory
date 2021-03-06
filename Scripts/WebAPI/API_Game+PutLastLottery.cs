﻿using System.Collections;
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

    public void PutLastLotteryRequest(string time, UnityAction callback)
    {
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        keyValuePairs.Add("lastLottery", time);
        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);

        client.Put(new Uri("https://www.pacheti.com/api/games/player/"), content, HttpCompletionOption.AllResponseContent, r =>
        {
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
