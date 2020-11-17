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
    private bool startLogin;
    [SerializeField]
    private string username;
    public string UsernameLogin { get { return username; } set { username = value; } }
    [SerializeField]
    private string password;
    public string PasswordLogin { get { return password; } set { password = value; } }
    public ResponseLogin m_Login;

    // formData.AddField("username", "atapydev01");
    // formData.AddField("password", "atapydev01");
    public void LoginWebRequest(UnityAction<string> callback)
    {
        Popup.Ins.PopupWaiting(true);
        HttpClient client = new HttpClient();

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        keyValuePairs.Add("username", username);
        keyValuePairs.Add("password", password);

        IHttpContent content = new FormUrlEncodedContent(keyValuePairs);
        
        client.Post(new Uri("https://www.pacheti.com/api/user/login/"), content, HttpCompletionOption.AllResponseContent, r =>
        {
            if (!r.IsSuccessStatusCode)
            {
                Debug.Log(r.ReadAsString());
                ReadStringReturn(r.ReadAsString());
                Popup.Ins.PopupOne("The username or password is incorrect.", "OK", null);
                // callback(r.ReadAsString());
                Popup.Ins.PopupWaiting(false);
            }
            else
            {
                Debug.Log(r.ReadAsString());
                m_Login = JsonUtility.FromJson<ResponseLogin>(r.ReadAsString());
                PlayerPrefs.SetString("token", m_Login.token);
                PlayerPrefs.SetString("slug", m_Login.slug);
                callback("Success");
            }
        });
        
    }


    [System.Serializable]
    public class ResponseLogin
    {
        public string result;
        public string token;
        public string username;
        public string slug;
        public string id;
    }
}



