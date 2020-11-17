using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CI.HttpClient;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public partial class API_Web : MonoBehaviour
{

    public static API_Web Ins { get { return instance; } }
    private static API_Web instance;

    void Awake()
    {
        if (API_Web.Ins != null && API_Web.Ins != this)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        
    }

    public void ReadStringReturn(string s)
    {
        int indexFoundA = s.IndexOf('[');
        int indexFoundB = s.IndexOf(']');

        if (indexFoundA < 0 || indexFoundA == indexFoundB || indexFoundB < 0)
            return;
        Popup.Ins.PopupOne(s.Substring(indexFoundA+2, (indexFoundB - indexFoundA)-3), "OK", null);
    }

    private void Update()
    {
        //Login
        if (startLogin)
        {
            //startLogin = false;
            //Debug.Log("Login");
            //LoginWebRequest();
        }
        //Logout
        if (startLogout)
        {
            //startLogout = false;
            //Debug.Log("Logout");
            //LogoutWebRequest();
        }
        //View profile
        if (startViewProfile)
        {
            //startViewProfile = false;
            //Debug.Log("View profile");
            //ViewProfileWebRequest();
        }
        //Register
        if (startRegister)
        {
            //startRegister = false;
            //Debug.Log("Register");
            //RegisterWebRequest();
        }
        //Edit profile
        if (startEditProfile)
        {
            startEditProfile = false;
            Debug.Log("Edit profile");
            EditProfileWebRequest();
        }
        //Add pet
        //if (startAddPet)
        //{
        //    startAddPet = false;
        //    Debug.Log("Add pet");
        //    AddPetWebRequest();
        //}
        //Edit pet
        //if (startEditPet)
        //{
        //    startEditPet = false;
        //    Debug.Log("Edit pet");
        //    EditPetWebRequest();
        //}
    }
}
