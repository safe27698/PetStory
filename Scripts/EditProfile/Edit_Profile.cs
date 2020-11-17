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

public class Edit_Profile : MonoBehaviour
{
    private API_Web web;
    private API_Game webG;

    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text countText;
    [SerializeField]
    private RawImage profileImage;
    [SerializeField]
    private Button loguotBtn;
    [SerializeField]
    private Button homeBtn;
    [SerializeField]
    private Button createPetBtn;
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private Edit_EditPet profilePet;
    [SerializeField]
    private GameObject popupEdit;
    [SerializeField]
    private Button editBtn;
    [SerializeField]
    private Button editAvatarBtn;
    [SerializeField]
    private Button deleteBtn;
    [SerializeField]
    private Button cancelBtn;


    public int petID;


    private void Awake()
    {
        web = GameObject.FindObjectOfType<API_Web>();
        webG = GameObject.FindObjectOfType<API_Game>();
        StartCoroutine(GetTexture(web.m_ViewProfile.user.profile.pic, profileImage));
        loguotBtn.onClick.AddListener(PreLogout);
        homeBtn.onClick.AddListener(GotoMainScene);
        createPetBtn.onClick.AddListener(CreateNewPet);
        nameText.text = web.m_ViewProfile.user.profile.slug;
        LoadItem();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SaveSystem.ResetSave();
    }
    void PreLogout()
    {
        Popup.Ins.PopupTwo("Do you want to logout?","OK","Cancel",Logout);
    }
    void Logout(bool bo)
    {
        if (!bo)
            return;

        web.LogoutWebRequest();
        SceneManager.LoadScene("Start");
    }

    IEnumerator GetTexture(string url, RawImage raw)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            try
            {
                raw.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
            catch
            {
                Debug.LogError("Missing");
            }
        }
    }

    void GotoMainScene()
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        Player player = SaveSystem.A_LoadSaveGame();

        for(int i = 0; i < player.pets.Count; i ++)
        {
            if (player.pets[i].selected && player.pets[i].status >= 0)
            {
                SceneManager.LoadScene("Main");
                return;
            }
        }

        Popup.Ins.PopupOne("Please select at least one pet.", "OK", null);
        
    }


    public void LoadItem()
    {
        Popup.Ins.PopupWaiting(true);

        countText.text = "0";
        int o = 0;
        while (item.transform.parent.childCount > 1)
        {
            Transform c = item.transform.parent.GetChild(o);
            if (c.name == item.name)
            {
                o++;
                continue;
            }
            c.parent = null;
            Destroy(c.gameObject);
        }
        
        for (int i = 0; i < web.m_ViewProfile.user.own.Length; i++)
        {
            GameObject clone = Instantiate(item, item.transform.parent);
            RawImage cloneRawImage = clone.GetComponentInChildren<RawImage>();
            //รับรูป
            StartCoroutine(GetTexture(web.m_ViewProfile.user.own[i].pic, cloneRawImage));
            //เซ็ตค่าตั้งต้น
            Text cloneText = clone.GetComponentInChildren<Text>();
            cloneText.text = web.m_ViewProfile.user.own[i].name;
            PetData petData = clone.AddComponent<PetData>();

            bool hasPet = false;
            for (int y = 0; y < webG.player.player.petList.Count; y++)
            {
                //ถ้าไม่มีในลิช
                if (webG.player.player.petList[y].gender.ToString() == web.m_ViewProfile.user.own[i].id)
                {
                    hasPet = true;
                    petData.ClonePetList(webG.player.player.petList[y]);
                    //เซ็ตอวตาร
                    if (petData.color >= 0)
                    {
                        Transform child = clone.transform.GetChild(2).GetChild(0);
                        child.gameObject.SetActive(true);
                        child.GetChild(0).GetComponent<Image>().sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.ear.color[petData.color])[petData.ear];
                        child.GetChild(1).GetComponent<Image>().sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.head.color[petData.color])[petData.head];
                        child.GetChild(2).GetComponent<Image>().sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.pattern.color[petData.color])[petData.pattern];
                        child.GetChild(3).GetComponent<Image>().sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.eye)[petData.eye];
                        child.GetChild(4).GetComponent<Image>().sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.eyebrow)[petData.eyebrow];
                        child.GetChild(5).GetComponent<Image>().sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.nose)[petData.nose];
                        child.GetChild(6).GetComponent<Image>().sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.mouth)[petData.mouth];
                    }
                    if (petData.selected && petData.status >= 0)
                    {
                        clone.transform.SetSiblingIndex(0);
                        clone.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
                        countText.text = (int.Parse(countText.text) + 1).ToString();
                    }
                    else
                    {
                        clone.transform.SetSiblingIndex(web.m_ViewProfile.user.own.Length - 1);
                    }
                    break;
                }
            }
            if (!hasPet)
            {
                API_Game.Ins.PostPetRequest(web.m_ViewProfile.user.own[i].name, web.m_ViewProfile.user.own[i].id, LoadItem);
                return;
            }

            //ถ้าไม่ถูกลบ
            if (petData.status >= 0)
                clone.SetActive(true);
        }
        Popup.Ins.PopupWaiting(false);
    }
    
    void CreateNewPet()
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        profilePet.gameObject.SetActive(true);
        profilePet.CreatePet();
    }
    
    public void EditPet()
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        profilePet.gameObject.SetActive(true);
        profilePet.EditPet();

        popupEdit.SetActive(false);
    }

    public void EditAvatar()
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        PlayerPrefs.SetInt("idPet", petID);
        SceneManager.LoadScene("CreateAvatar");
    }

    public void DeletePet()
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        Popup.Ins.PopupTwo("Do you want to delete this pet?", "OK","Cancel",CallbackDelete);
    }

    public void CallbackDelete(bool bo)
    {
        if (!bo)
            return;
        Player p = SaveSystem.A_LoadSaveGame();
        for (int i = 0; i < p.pets.Count; i++)
        {
            if (petID == p.pets[i].id)
            {

                p.pets[i].status = -1;
                PetData petData = new PetData();
                petData.CloneData(p.pets[i]);
                SaveSystem.A_EditPet(petData,true, LoadItem);
                popupEdit.SetActive(false);
            }
        }
    }

    public void Cancel()
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        popupEdit.SetActive(false);
    }

    public void OpenEdit(GameObject obj)
    {
        PetData petData = obj.GetComponent<PetData>();
        petID = petData.id ;
        popupEdit.SetActive(true);
        editBtn.onClick.AddListener(EditPet);
        editAvatarBtn.onClick.AddListener(EditAvatar);
        cancelBtn.onClick.AddListener(Cancel);
    }

    public void Selection(GameObject obj)
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        PetData p = obj.GetComponent<PetData>();
        if (int.Parse(countText.text) >= 5 && !p.selected)
        {
            Popup.Ins.PopupOne("Cannot select more than 5 characters.", "OK", null);
            return;
        }

        if (p.color == -1)
        {
            Popup.Ins.PopupOne("Please create an avatar for this pet first.", "OK",null);
            return;
        }

        Popup.Ins.PopupWaiting(true);
        int countIndex = p.transform.parent.childCount;
        int lastSelect = 0;
        for (int i = 0; i < countIndex ; i++)
        {
            PetData pp = obj.transform.parent.GetChild(i).GetComponent<PetData>();
            if (pp == null)
            {
                continue;
            }
            if (pp.selected)
            {
                lastSelect = i;
                continue;
            }
        }

        p.selected = !p.selected;
        if (p.selected)
        {
            countText.text = (int.Parse(countText.text) + 1).ToString();
            p.transform.SetSiblingIndex(lastSelect + 1);
        }
        else
        {
            countText.text = (int.Parse(countText.text) - 1).ToString();
            p.transform.SetSiblingIndex(lastSelect);
        }


        p.transform.GetChild(4).GetChild(0).gameObject.SetActive(p.selected);

        SaveSystem.A_EditPet(p,true);
    }
}
