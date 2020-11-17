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
    int petId;
    public void PutPetRequest(PetData pet, UnityAction callback)
    {
        Debug.Log("PutPetRequest");
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();

        foreach (PetList p in player.player.petList)
        {
            if (p.gender == pet.id)
                petId = p.id;
        }
        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        
        keyValuePairs.Add("name", pet.namePet);
        keyValuePairs.Add("gender", pet.gender.ToString());
        keyValuePairs.Add("hunger", pet.hunger.ToString());
        keyValuePairs.Add("happiness", pet.happiness.ToString());
        keyValuePairs.Add("uuid", pet.UUID.ToString());
        keyValuePairs.Add("color", pet.color.ToString());
        keyValuePairs.Add("head", pet.head.ToString());
        keyValuePairs.Add("ear", pet.ear.ToString());
        keyValuePairs.Add("eye", pet.eye.ToString());
        keyValuePairs.Add("eyebrow", pet.eyebrow.ToString());
        keyValuePairs.Add("nose", pet.nose.ToString());
        keyValuePairs.Add("mouth", pet.mouth.ToString());
        keyValuePairs.Add("pattern", pet.pattern.ToString());
        keyValuePairs.Add("accessories", pet.accessoriesWearing.ToString());
        keyValuePairs.Add("shirt", pet.shirtWearing.ToString());
        keyValuePairs.Add("pant", pet.pantWearing.ToString());
        keyValuePairs.Add("shoe", pet.shoeWearing.ToString());
        keyValuePairs.Add("selected", pet.selected.ToString());
        keyValuePairs.Add("status", pet.status.ToString());
        keyValuePairs.Add("lastTimeReward", player.player.lastLogin);

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);
        //string jsonString = "";

        //foreach (KeyValuePair<string, string> k in keyValuePairs)
        //{
        //    jsonString += "\""+k.Key+ "\"" + ":" + "\"" + k.Value+ "\""+",";
        //}
        //jsonString = jsonString.Remove(jsonString.Length-1);
        //jsonString = "{" + jsonString + "}";
        //IHttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
        
        client.Put(new Uri("https://www.pacheti.com/api/games/pet/"+ petId + "/"), content, HttpCompletionOption.AllResponseContent, r =>
        {
            Popup.Ins.PopupWaiting(false);
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                Popup.Ins.PopupWaiting(true);
                Debug.Log(r.ReadAsString());
                GetPlayerRequest(callback);
            }
        });

    }

}
