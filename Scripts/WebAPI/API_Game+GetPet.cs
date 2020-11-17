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

    private RootPet petlist;

    public void GetPetRequest(UnityAction callback)
    {
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));
      //  client.Headers.Add("Content-Type", "application/json");

        client.Get(new Uri("https://www.pacheti.com/api/games/pet/"), HttpCompletionOption.AllResponseContent, r =>
        {
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                petlist = JsonUtility.FromJson<RootPet>(r.ReadAsString());
                player.player.petList = petlist.result;
                Debug.Log(r.ReadAsString());
                callback();
            }
        });

    }

}
