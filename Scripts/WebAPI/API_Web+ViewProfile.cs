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
    private bool startViewProfile;
    [SerializeField]
    public ResponseViewProfile m_ViewProfile;

    public void ViewProfileWebRequest(UnityAction callback)
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();

        client.Headers.Add("Authorization", "Token " + PlayerPrefs.GetString("token"));

        client.Get(new Uri("https://www.pacheti.com/api/user/" + PlayerPrefs.GetString("slug") + "/"), HttpCompletionOption.AllResponseContent, r =>
        {
            //Popup.Ins.PopupWaiting(false);
            if (!r.IsSuccessStatusCode)
            {
                Popup.Ins.PopupWaiting(false);
                Debug.Log(r.ReadAsString());
                Popup.Ins.PopupOne("The username or password is incorrect.","OK",null);
            }
            else
            {
                Debug.Log(r.ReadAsString());
                m_ViewProfile = JsonUtility.FromJson<ResponseViewProfile>(r.ReadAsString());
                callback();
            }
        });
        
    }


    [System.Serializable]
    public class ResponseViewProfile
    {
        public User user;
    }

    [System.Serializable]
    public class User
    {
        public string username;
        public Own[] own;
        public Profile profile;
    }

    [System.Serializable]
    public class Own
    {
        public string id;
        public string name;
        public string breed;
        public string pettype;
        public string bloodtype;
        public string gender;
        public string birthdate;
        public string furcolor;
        public string weight;
        public string pic;
        public string devices_pet;
        public string[] petlocations;
        public int status;
    }

    [System.Serializable]
    public class Profile
    {
        public int id;
        public string oneid;
        public string oneemail;
        public string slug;
        public string account_title_th;
        public string account_title_en;
        public string first_name_th;
        public string last_name_th;
        public string first_name_en;
        public string last_name_en;
        public string gender;
        public string pic;
        public string mobile_no;
        public string email;
        public string birthdate;
        public string address;
        public string zipcode;
        public string update;
        public string province;
        public string districts;
        public string subdistrict;
        public string country;
    }

}
