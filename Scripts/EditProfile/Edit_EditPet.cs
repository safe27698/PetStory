using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CI.HttpClient;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Edit_EditPet : MonoBehaviour
{
    [SerializeField]
    private InputField nameText;

    [SerializeField]
    private Dropdown type;

    [SerializeField]
    private InputField breed;

    [SerializeField]
    private Dropdown gender;
    
    [SerializeField]
    private InputField age;
    
    [SerializeField]
    private InputField birthdayYear;
    [SerializeField]
    private InputField birthdayMonth;
    [SerializeField]
    private InputField birthdayDay;

    [SerializeField]
    private InputField weight;

    [SerializeField]
    private InputField color;

    [SerializeField]
    private Button okBtn;
    [SerializeField]
    private Button backBtn;
    [SerializeField]
    private RectTransform content;

    private API_Web web;
    private API_Game webG;
    private Edit_Profile profile;
    

    private void Awake()
    {
        web = GameObject.FindObjectOfType<API_Web>();
        webG = GameObject.FindObjectOfType<API_Game>();
        profile = GameObject.FindObjectOfType<Edit_Profile>();
        backBtn.onClick.AddListener(PreBack);

    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        content.anchoredPosition = new Vector2(content.anchoredPosition.x,0);
    }
    
    public void CreatePet()
    {

        nameText.text = "";
        type.value = 0;
        breed.text = "";
        gender.value = 0;
        age.text = "";
        birthdayYear.text = "";
        birthdayMonth.text = "";
        birthdayDay.text = "";
        weight.text = "";
        color.text = "";
        okBtn.onClick.AddListener(Create);
    }
    
    void Create()
    {
       
        if (nameText.text.Length <= 0)
        {
            Popup.Ins.PopupOne("Please enter all information.", "OK", null);
            return;
        }
        web.NamepetAdd = nameText.text;

        if (type.options[type.value].text.Length <= 0)
        {
            Popup.Ins.PopupOne("Please enter all information.", "OK", null);
            return;
        }
        web.TypeAdd = type.options[type.value].text;

        if (breed.text.Length <= 0)
        {
            Popup.Ins.PopupOne("Please enter all information.", "OK", null);
            return;
        }
        web.BreedAdd = breed.text;

        web.AgeAdd = age.text;

       
        if (weight.text.Length <= 0)
        {
            Popup.Ins.PopupOne("Please enter all information.", "OK", null);
            return;
        }
        web.WeightAdd = weight.text;

        if (color.text.Length <= 0)
        {
            Popup.Ins.PopupOne("Please enter all information.", "OK", null);
            return;
        }
        web.ColorAdd = color.text;
        try
        {
            web.BirthdayAdd = System.DateTime.Parse(birthdayYear.text + "-" + birthdayMonth.text + "-" + birthdayDay.text).ToString("yyyy-MM-dd");

        }
        catch
        {
            Popup.Ins.PopupOne("Plase enter your birthday correctly.", "OK", null);
            return;
        }
        web.AddPetWebRequest(PreBack);
    }

    public void EditPet ()
    {
        for (int i = 0; i < web.m_ViewProfile.user.own.Length; i++)
        {
            if (web.m_ViewProfile.user.own[i].id != profile.petID.ToString())
                continue;
            nameText.text = web.m_ViewProfile.user.own[i].name;
            type.value = 0;
            breed.text = web.m_ViewProfile.user.own[i].breed;
            gender.value = 0;
            age.text = "";
            birthdayYear.text = System.DateTime.Parse(web.m_ViewProfile.user.own[i].birthdate).ToString("yyyy");
            birthdayMonth.text = System.DateTime.Parse(web.m_ViewProfile.user.own[i].birthdate).ToString("MM");
            birthdayDay.text = System.DateTime.Parse(web.m_ViewProfile.user.own[i].birthdate).ToString("dd");
            weight.text = web.m_ViewProfile.user.own[i].weight;
            color.text = web.m_ViewProfile.user.own[i].furcolor;
            okBtn.onClick.AddListener(Edit);
        }
    }

    void Edit()
    {
        try
        {
            web.NamepetEdit = nameText.text;
            web.TypeEdit = type.options[type.value].text;
            web.BreedEdit = breed.text;
            web.GenderEdit = gender.options[gender.value].text;
            web.AgeEdit = age.text;
            web.BirthdayEdit = System.DateTime.Parse(birthdayYear.text + "-" + birthdayMonth.text + "-" + birthdayDay.text).ToString("yyyy-MM-dd");
            web.WeightEdit = weight.text;
            web.ColorEdit = color.text;
        }
        catch
        {
            Popup.Ins.PopupOne("You have entered incorrect information.", "OK", null);
            return;
        }

        web.EditPetWebRequest(profile.petID.ToString(), PreBack);
    }

    void PreBack()
    {
        web.ViewProfileWebRequest(Back);

    }
    void Back()
    {
        profile.LoadItem();
        //string nPet = web.m_ViewProfile.user.own[web.m_ViewProfile.user.own.Length - 1].name;
        //string iPet = web.m_ViewProfile.user.own[web.m_ViewProfile.user.own.Length - 1].id;
        //webG.PostPetRequest(nPet, iPet, profile.LoadItem);
        okBtn.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }

}
