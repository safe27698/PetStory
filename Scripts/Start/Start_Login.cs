using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System;

public class Start_Login : MonoBehaviour
{
    [SerializeField]
    private InputField user;
    [SerializeField]
    private InputField pass;
    [SerializeField]
    private Button loginBtn;
    [SerializeField]
    private Button registerBtn;
    [SerializeField]
    private GameObject register;

    private API_Web web;
    private API_Game webG;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        web = GameObject.FindObjectOfType<API_Web>();
        webG = GameObject.FindObjectOfType<API_Game>();
        loginBtn.onClick.AddListener(Login);
        registerBtn.onClick.AddListener(Register);

        if (PlayerPrefs.HasKey("token"))
        {
            Result("");
        }
    }

    public void Login()
    {
        web.UsernameLogin = user.text;
        web.PasswordLogin = pass.text;
        web.LoginWebRequest(Result);
    }

    public void Result(string result)
    {
        web.ViewProfileWebRequest(API_Game);
    }

    public void API_Game()
    {
        webG.GetPlayerRequest (PreFinish);
    }

    public void PreFinish()
    {
        Player player = SaveSystem.A_LoadSaveGame();
        Finish();
    }

    public void Finish()
    {
        Player player = SaveSystem.A_LoadSaveGame();
        if (player.pets != null && player.pets.Count > 0)
        {
            for (int i = 0; i < player.pets.Count; i++)
            {
                if (player.pets[i].selected && player.pets[i].status >= 0)
                {
                    StartCoroutine(LoadYourAsyncScene("Main"));
                    return;
                }
            }
        }
        if (player.coin == 0 && (player.pets == null || player.pets.Count <= 0))
        {
            SaveSystem.A_AddCoin(2500);
        }


        StartCoroutine(LoadYourAsyncScene("Edit"));
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
            gameObject.SetActive(false);
            Popup.Ins.PopupWaiting(false);
        }

    }

    public void Register()
    {
        register.SetActive(true);
    }
}
