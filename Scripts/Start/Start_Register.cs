using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class Start_Register : MonoBehaviour
{
    [SerializeField]
    private InputField user;
    [SerializeField]
    private InputField pass;
    [SerializeField]
    private InputField confrimpass;
    [SerializeField]
    private InputField tel;
    [SerializeField]
    private Button registerBtn;
    [SerializeField]
    private Button backBtn;
    [SerializeField]
    private Text aleart;

    private API_Web web;
    // Start is called before the first frame update
    void Start()
    {
        web = GameObject.FindObjectOfType<API_Web>();
        registerBtn.onClick.AddListener(Register);
        backBtn.onClick.AddListener(Back);
    }

    void Register()
    {
        if (user.text.Length < 4)
        {
            aleart.text = "Invalid username";
            Debug.Log("Invalid username");
            return;
        }
        if (pass.text.Length < 8)
        {
            aleart.text = "Incorrect password";
            Debug.Log("Invalid password");
            return;
        }
        if (pass.text != confrimpass.text)
        {
            aleart.text = "Passwords do not match";
            Debug.Log("Passwords do not match");
            return;
        }
        if (tel.text.Length < 9)
        {
            aleart.text = "Invalid phone number";
            Debug.Log("Invalid phone number");
            return;
        }
        aleart.text = "";
        web.UsernameRegis = user.text;
        web.PasswordRegis = pass.text;
        web.Tel = tel.text;
        web.RegisterWebRequest(Result);
    }

    void Result(string result)
    {
        gameObject.SetActive(false);
    }

    void Back()
    {
        gameObject.SetActive(false);
    }
}

