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
    private bool startRegister;
    [SerializeField]
    private string usernameRegis;
    public string UsernameRegis { get { return usernameRegis; } set { usernameRegis = value; } }
    [SerializeField]
    private string passwordRegis;
    public string PasswordRegis { get { return passwordRegis; } set { passwordRegis = value; } }
    [SerializeField]
    private string tel;
    public string Tel { get { return tel; } set { tel = value; } }

    public ResponseRegister m_Register;
    
    public void RegisterWebRequest(UnityAction<string> callback)
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        keyValuePairs.Add("username", usernameRegis);
        keyValuePairs.Add("password", passwordRegis);
        keyValuePairs.Add("mobile_no", tel);

        keyValuePairs.Add("account_title_th", "");
        keyValuePairs.Add("account_title_en", "");
        keyValuePairs.Add("first_name_th", "");
        keyValuePairs.Add("last_name_th", "");
        keyValuePairs.Add("first_name_en", "");
        keyValuePairs.Add("last_name_en", "");
        keyValuePairs.Add("gender", "");
        keyValuePairs.Add("pic", "");
        keyValuePairs.Add("email", "");
        //keyValuePairs.Add("birthdate", "");

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);
        
        client.Post(new Uri("https://www.pacheti.com/api/user/signup/"), content, HttpCompletionOption.AllResponseContent, r =>
        {
            Popup.Ins.PopupWaiting(false);
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
                ReadStringReturn(r.ReadAsString());
                //callback(r.ReadAsString());
            }
            else
            {
                Debug.Log(r.ReadAsString());
                m_Register = JsonUtility.FromJson<ResponseRegister>(r.ReadAsString());
                Popup.Ins.PopupOne("Successfully registered.","OK",null);
                callback("Success");
            }
        });
    }

    [System.Serializable]
    public class ResponseRegister
    {
        public string result;
        public string msg;
        public string token;
        public string username;
        public string slug;
        public string id;
    }
}
