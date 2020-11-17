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
    private string namePetEdit;
    public string NamepetEdit { get { return namePetEdit; } set { namePetEdit = value; } }
    [SerializeField]
    private string typeEdit;
    public string TypeEdit { get { return typeEdit; } set { typeEdit = value; } }
    [SerializeField]
    private string breedEdit;
    public string BreedEdit { get { return breedEdit; } set { breedEdit = value; } }
    [SerializeField]
    private string genderEdit;
    public string GenderEdit { get { return genderEdit; } set { genderEdit = value; } }
    [SerializeField]
    private string ageEdit;
    public string AgeEdit { get { return ageEdit; } set { ageEdit = value; } }
    [SerializeField]
    private string birthdayEdit;
    public string BirthdayEdit { get { return birthdayEdit; } set { birthdayEdit = value; } }
    [SerializeField]
    private string weightEdit;
    public string WeightEdit { get { return weightEdit; } set { weightEdit = value; } }
    [SerializeField]
    private string colorEdit;
    public string ColorEdit { get { return colorEdit; } set { colorEdit = value; } }

    public void EditPetWebRequest(string idPet, UnityAction callback)
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        keyValuePairs.Add("name", namePetEdit);
        keyValuePairs.Add("pettype", typeEdit);
        keyValuePairs.Add("breed", breedEdit);
        keyValuePairs.Add("gendaer", genderEdit);
        keyValuePairs.Add("birthdate", birthdayEdit);
        keyValuePairs.Add("weight", weightEdit);
        keyValuePairs.Add("furcolor", colorEdit);

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);

        client.Patch(new Uri("https://www.pacheti.com/api/pets/" + idPet + "/edit/"), content, HttpCompletionOption.AllResponseContent, r =>
        {
            Popup.Ins.PopupWaiting(false);
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
            }
            else
            {
                Debug.Log(r.ReadAsString());
                PetData petData = SaveSystem.A_LoadPet(int.Parse(idPet));
                petData.namePet = namePetEdit;
                API_Game.Ins.PutPetRequest(petData, callback);
               // callback();
            }
        });

    }

}
