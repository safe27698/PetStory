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
    public void PostPetRequest (string name, string id, UnityAction callback)
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        keyValuePairs.Add("name", name.ToString());
        keyValuePairs.Add("gender", id);
        keyValuePairs.Add("hunger","0");
        keyValuePairs.Add("happiness", "0");
        keyValuePairs.Add("uuid", "0");
        keyValuePairs.Add("color", "-1");
        keyValuePairs.Add("head", "0");
        keyValuePairs.Add("ear", "0");
        keyValuePairs.Add("eye", "0");
        keyValuePairs.Add("eyebrow", "0");
        keyValuePairs.Add("nose", "0");
        keyValuePairs.Add("mouth", "0");
        keyValuePairs.Add("pattern", "0");
        keyValuePairs.Add("accessories", "-1");
        keyValuePairs.Add("shirt", "-1");
        keyValuePairs.Add("pant", "-1");
        keyValuePairs.Add("shoe", "-1");
        keyValuePairs.Add("selected", "false");
        keyValuePairs.Add("status", "0");
       // keyValuePairs.Add("lastTimeReward", "0");

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);

        client.Post(new Uri("https://www.pacheti.com/api/games/pet/"), content, HttpCompletionOption.AllResponseContent, r =>
         {
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                Debug.Log(r.ReadAsString());
                 GetPlayerRequest(callback);
                // callback();
            }
        });

    }

}
