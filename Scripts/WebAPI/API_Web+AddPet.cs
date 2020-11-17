using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CI.HttpClient;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using UnityEngine.Events;

public partial class API_Web : MonoBehaviour
{
    [Space]
    [SerializeField]
    private string namePetAdd;
    public string NamepetAdd { get { return namePetAdd; } set { namePetAdd = value; } }
    [SerializeField]
    private string typeAdd;
    public string TypeAdd { get { return typeAdd; } set { typeAdd = value; } }
    [SerializeField]
    private string breedAdd;
    public string BreedAdd { get { return breedAdd; } set { breedAdd = value; } }
    [SerializeField]
    private string genderAdd;
    public string GenderAdd { get { return genderAdd; } set { genderAdd = value; } }
    [SerializeField]
    private string ageAdd;
    public string AgeAdd { get { return ageAdd; } set { ageAdd = value; } }
    [SerializeField]
    private string birthdayAdd;
    public string BirthdayAdd { get { return birthdayAdd; } set { birthdayAdd = value; } }
    [SerializeField]
    private string weightAdd;
    public string WeightAdd { get { return weightAdd; } set { weightAdd = value; } }
    [SerializeField]
    private string colorAdd;
    public string ColorAdd { get { return colorAdd; } set { colorAdd = value; } }
    
    public void AddPetWebRequest(UnityAction callback)
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();
        Debug.Log("Add Pet");
        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        string[] realuser = m_ViewProfile.user.profile.slug.ToString().Split('-');

        keyValuePairs.Add("name", namePetAdd);
        keyValuePairs.Add("pettype", typeAdd);
        keyValuePairs.Add("breed", breedAdd);
        keyValuePairs.Add("gendaer", genderAdd);
        keyValuePairs.Add("birthdate", birthdayAdd);
        keyValuePairs.Add("furcolor", colorAdd);
        keyValuePairs.Add("weight", weightAdd);
        keyValuePairs.Add("realuser", realuser[0]) ;
        keyValuePairs.Add("shareuser", realuser[0]);

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);

        client.Post(new Uri("https://www.pacheti.com/api/pets/addpet/"), content, HttpCompletionOption.AllResponseContent, r =>
        {
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                Debug.Log(r.ReadAsString());
                callback();
            }
        });

    }

}
