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

    private int furIdList;
    
    public void PutListFurnitureRequest(List<FurnitureData> furList, UnityAction callback)
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();
        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        string jsonString = "";
        for (int i = 0; i < furList.Count; i++)
        {
            jsonString += "{";
            jsonString += "\"" + "id" + "\"" + ":" + "\"" + furList[i].realId + "\"" + ",";
            jsonString += "\"" + "using" + "\"" + ":" + "\"" + furList[i].furnitureIsUsing + "\"" + ",";
            jsonString += "\"" + "type" + "\"" + ":" + "\"" + furList[i].type + "\"" + ",";
            jsonString += "\"" + "vx" + "\"" + ":" + "\"" + furList[i].position.x + "\"" + ",";
            jsonString += "\"" + "vy" + "\"" + ":" + "\"" + furList[i].position.y + "\"" + ",";
            jsonString += "\"" + "vz" + "\"" + ":" + "\"" + furList[i].position.z + "\"" + ",";
            jsonString += "\"" + "qx" + "\"" + ":" + "\"" + furList[i].rotation.x + "\"" + ",";
            jsonString += "\"" + "qy" + "\"" + ":" + "\"" + furList[i].rotation.y + "\"" + ",";
            jsonString += "\"" + "qz" + "\"" + ":" + "\"" + furList[i].rotation.z + "\"" + ",";
            jsonString += "\"" + "qw" + "\"" + ":" + "\"" + furList[i].rotation.w + "\"";
            jsonString += "},";
        }
        jsonString = jsonString.Remove(jsonString.Length-1);
        jsonString = "[" + jsonString + "]";
        // IHttpContent content = new FormUrlEncodedContent(keyValuePairs);
//        Debug.LogError(jsonString);
        IHttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

        client.Put(new Uri("https://www.pacheti.com/api/games/updateallfurniture/"), content, HttpCompletionOption.AllResponseContent, r =>
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
