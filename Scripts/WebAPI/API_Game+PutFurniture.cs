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

    int furId;
    
    public void PutFurnitureRequest(Furniture fur , UnityAction callback)
    {
        HttpClient client = new HttpClient();

        foreach (Inventoryfurniture f in player.player.inventoryfurniture)
        {
            if (f.furnitureid == fur.id)
                furId = f.id;
        }
        Debug.LogError(fur.id+" // "+fur.type+" // "+ fur.furnitureIsUsing);
        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        keyValuePairs.Add("furnitureid", fur.id.ToString());
        keyValuePairs.Add("using", fur.furnitureIsUsing.ToString());
        keyValuePairs.Add("type", fur.type.ToString());
        keyValuePairs.Add("vx", fur.position.x.ToString());
        keyValuePairs.Add("vy", fur.position.y.ToString());
        keyValuePairs.Add("vz", fur.position.z.ToString());
        keyValuePairs.Add("qx", fur.rotation.x.ToString());
        keyValuePairs.Add("qy", fur.rotation.y.ToString());
        keyValuePairs.Add("qz", fur.rotation.z.ToString());
        keyValuePairs.Add("qw", fur.rotation.w.ToString());

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);

        client.Put(new Uri("https://www.pacheti.com/api/games/furniture/"+ furId .ToString()+ "/"), content, HttpCompletionOption.AllResponseContent, r =>
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
