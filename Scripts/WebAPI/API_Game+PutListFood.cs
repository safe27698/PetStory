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
    
    public void PutListFood(Dictionary<int, int> furList, UnityAction callback)
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();
        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        string jsonString = "";
        foreach (KeyValuePair<int, int> fur in furList)
        {
            jsonString += "{";
            jsonString += "\"" + "id" + "\"" + ":" + "\"" + fur.Key + "\"" + ",";
            jsonString += "\"" + "count" + "\"" + ":" + "\"" + fur.Value + "\"";
            jsonString += "},";
        }
        jsonString = jsonString.Remove(jsonString.Length-1);
        jsonString = "[" + jsonString + "]";
        // IHttpContent content = new FormUrlEncodedContent(keyValuePairs);
        Debug.LogError(jsonString);
        IHttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

        client.Put(new Uri("https://www.pacheti.com/api/games/updateallfood/"), content, HttpCompletionOption.AllResponseContent, r =>
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
                //callback();
            }
          });

    }
}
