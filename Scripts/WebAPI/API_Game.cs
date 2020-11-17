using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CI.HttpClient;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;


// cd C:\Users\Safe\Desktop\Pacheti3\dev\myproject
//python manage.py runserver 0.0.0.0:80

public partial class API_Game : MonoBehaviour
{

    public static API_Game Ins { get { return instance; } }
    private static API_Game instance;

    void Awake()
    {
        if (API_Game.Ins != null && API_Game.Ins != this)
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
        Popup.Ins.PopupOne(s.Substring(indexFoundA + 2, (indexFoundB - indexFoundA) - 3), "OK", null);
    }

    private void Update()
    {

    }


    [System.Serializable]
    public class PetList
    {
        public int id ;
        public string name ;
        public int gender ;
        public int hunger ;
        public int happiness ;
        public string uuid ;
        public int color ;
        public int head ;
        public int ear ;
        public int eye ;
        public int eyebrow ;
        public int nose ;
        public int mouth ;
        public int pattern ;
        public int accessories ;
        public int shirt ;
        public int pant ;
        public int shoe ;
        public bool selected ;
        public int status ;
        public string lastTimeReward ;
        public int idplayer ;
    }
    [System.Serializable]
    public class RootPet
    {
        public List<PetList> result;
    }

    [System.Serializable]
    public class Inventorycloth
    {
        public int id ;
        public int clothid ;
        public int count ;
        public int type ;
        public int inventory_player ;
    }
    [System.Serializable]
    public class RootInventorycloth
    {
        public List<Inventorycloth> inventorycloths ;
    }

    [System.Serializable]
    public class Inventoryfood
    {
        public int id ;
        public int foodid ;
        public int count ;
        public int foodinventory_player ;
    }
    [System.Serializable]
    public class RootInventoryfood
    {
        public List<Inventoryfood> inventoryfood;
    }
    [System.Serializable]
    public class Inventoryfurniture
    {
        public int id ;
        public int furnitureid ;
        public bool @using ;
        public int type ;
        public float vx ;
        public float vy ;
        public float vz ;
        public float qw ;
        public float qx ;
        public float qy ;
        public float qz ;
        public int furnitureinventory_player ;
    }

    [System.Serializable]
    public class Player
    {
        public int id ;
        public int idPetdy ;
        public int coin ;
        public string lastLogin ;
        public string lastLottery ;
        public List<PetList> petList ;
        public List<Inventorycloth> inventorycloth ;
        public List<Inventoryfood> inventoryfood ;
        public List<Inventoryfurniture> inventoryfurniture;
    }

    [System.Serializable]
    public class RootPlayer
    {
        public Player player ;
    }

    [System.Serializable]
    public class PostPatchPet
    {
        public string name ;
        public int gender ;
        public string hunger ;
        public string happiness ;
        public string uuid ;
        public string color ;
        public string head ;
        public string ear ;
        public string eye ;
        public string eyebrow ;
        public string nose ;
        public string mouth ;
        public string pattern ;
        public string accessories ;
        public string shirt ;
        public string pant ;
        public string shoe ;
        public bool selected ;
        public string statuspet ;
        public string lastTimeReward ;
    }

    [System.Serializable]
    public class PostCloth
    {
        public string clothid ;
        public string type ;
    }

    [System.Serializable]
    public class PatchPostFood
    {
        public int foodid ;
        public int count ;
    }

    [System.Serializable]
    public class PostPatchFurniture
    {
        public string furniture ;
        public bool @using ;
        public string type ;
        public string vx ;
        public string vy ;
        public string vz ;
        public string qx ;
        public string qy ;
        public string qz ;
        public string qw ;
    }

    [System.Serializable]
    public class PatchCoin
    {
        public int coin ;
    }
    
}
