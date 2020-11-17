
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{

    public static string mainSave = "MainSave";

    [SerializeField]
    private Player p;

    public static SaveSystem Ins { get { return instance; } }
    private static SaveSystem instance;

    void Awake()
    {
        if (SaveSystem.Ins != null && SaveSystem.Ins != this)
        {

            Destroy(transform.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
        ResetSave();
    }

    private void Update()
    {
        // p = Loaded();
    }

    private static void Init()
    {
        if (!PlayerPrefs.HasKey(mainSave))
        {
            //Player m = new Player();
            //m.coin += 1000;
            //string newSave = JsonUtility.ToJson(m);
            //PlayerPrefs.SetString(mainSave, newSave);
            //A_AddFurniture(1, 4);
            //A_AddFurniture(2, 3);
            //FurnitureData f = new FurnitureData();
            //f.id = 1;
            //f.type = 4;
            //A_SaveFurniture(f);
            //FurnitureData f2 = new FurnitureData();
            //f2.id = 2;
            //f2.type = 3;
            //A_SaveFurniture(f2);
            //A_AddFood(2);
            //A_AddFood(2);
        }

    }
    public static void ResetSave()
    {
        PlayerPrefs.DeleteKey(mainSave);
        Player m = new Player();
        string newSave = JsonUtility.ToJson(m);
        PlayerPrefs.SetString(mainSave, newSave);
    }


    private static void Saved(Player m)
    {
        string json = JsonUtility.ToJson(m);
        PlayerPrefs.SetString(mainSave, json);
        SaveSystem.Ins.p = m;
    }

    private static Player Loaded()
    {

        Player m_Save = new Player();
        m_Save.coin = API_Game.Ins.player.player.coin;
        m_Save.lastTimeInGame = string.Format("{0:yyyy-MM-dd}", API_Game.Ins.player.player.lastLogin);
        m_Save.lastTimeLotteyry = string.Format("{0:yyyy-MM-dd}", API_Game.Ins.player.player.lastLottery);
        m_Save.pets = new List<Pet>();
        for (int i = 0; i < API_Game.Ins.player.player.petList.Count; i++)
        {
            API_Game.PetList p = API_Game.Ins.player.player.petList[i];
            Pet pet = new Pet();

            pet.id = p.gender;
            pet.namePet = p.name;
            pet.gender = p.gender;
            pet.hunger = p.hunger;
            pet.happiness = p.happiness;
            pet.UUID = p.uuid.ToString();
            pet.selected = p.selected;
            pet.status = p.status;
            pet.lastTimeReward = p.lastTimeReward.ToString();

            pet.color = p.color;
            pet.head = p.head;
            pet.ear = p.ear;
            pet.eye = p.eye;
            pet.eyebrow = p.eyebrow;
            pet.nose = p.nose;
            pet.mouth = p.mouth;
            pet.pattern = p.pattern;

            pet.accessoriesWearing = p.accessories;
            pet.shirtWearing = p.shirt;
            pet.pantWearing = p.pant;
            pet.shoeWearing = p.shoe;
            
            m_Save.pets.Add(pet);

        }
        m_Save.inventory = new Inventory();
        m_Save.inventory.clothes = new List<Clothes>();
        for (int i = 0; i < API_Game.Ins.player.player.inventorycloth.Count; i++)
        {
            API_Game.Inventorycloth c = API_Game.Ins.player.player.inventorycloth[i];
            Clothes clothes = new Clothes();

            clothes.id = c.clothid;
            clothes.type = c.type;
            clothes.count = c.count;

            m_Save.inventory.clothes.Add(clothes);

        }
        m_Save.inventory.furniture = new List<Furniture>();
        for (int i = 0; i < API_Game.Ins.player.player.inventoryfurniture.Count; i++)
        {
            API_Game.Inventoryfurniture c = API_Game.Ins.player.player.inventoryfurniture[i];
            Furniture furniture = new Furniture();

            furniture.id = c.furnitureid;
            furniture.realId = c.id;
            furniture.type = c.type;
            furniture.furnitureIsUsing = c.@using;
            furniture.position = new Vector3(c.vx, c.vy, c.vz);
            furniture.rotation = new Quaternion(c.qx, c.qy, c.qz, c.qw);

            m_Save.inventory.furniture.Add(furniture);
        }
        m_Save.inventory.foods = new List<Food>();
        for (int i = 0; i < API_Game.Ins.player.player.inventoryfood.Count; i++)
        {
            API_Game.Inventoryfood c = API_Game.Ins.player.player.inventoryfood[i];
            Food food = new Food();

            food.id = c.foodid;
            food.count = c.count;

            m_Save.inventory.foods.Add(food);

        }
        SaveSystem.Ins.p = m_Save;
        return m_Save;
    }


    public static Player A_LoadSaveGame()
    {
        // Init();
        return Loaded();
    }

    public static void A_SaveGame(Player p)
    {
        Saved(p);
    }
    public static void A_SaveLotterry(string date)
    {
        API_Game.Ins.PutLastLotteryRequest(date, null);
    }

    //public static string A_Time()
    //{
    //    return System.DateTime.Now.ToLongDateString()+ "/" + System.DateTime.Now.ToLongTimeString();
    //}

    public static bool A_CreatePet(PetData pet)
    {
        Player player = Loaded();

        for (int i = 0; i < player.pets.Count; i++)
        {
            if (player.pets[i].id == pet.id)
            {
                return false;
            }
        }

        player.pets.Add(pet.ReturnPet(pet));
        //  API_Game.Ins.PostPetRequest(player.pets.Last().namePet, player.pets.Last().id, null);
        Saved(player);

        return true;
    }

    public static PetData A_LoadPet(int id)
    {
        PetData petData = new PetData();
        Player player = Loaded();

        for (int i = 0; i < player.pets.Count; i++)
        {
            if (player.pets[i].id == id)
            {
                petData.CloneData(player.pets[i]);
                return petData;
            }
        }
        Debug.LogError("Not found a pet");
        return null;
    }

    public static bool A_EditPet(PetData pet ,bool save, UnityAction action = null)
    {
        Player player = Loaded();

        for (int i = 0; i < player.pets.Count; i++)
        {
            if (player.pets[i].id == pet.id)
            {
                player.pets[i] = pet.ReturnPet(pet);
                if(save)
                    API_Game.Ins.PutPetRequest(pet, action);
                // Popup.Ins.PopupWaiting(false);
            }
        }

        Saved(player);

        return true;
    }

    public static void A_AddCoin(int coin)
    {
        Player player = Loaded();
        player.coin += coin;
        API_Game.Ins.PutCoinRequest(player.coin, null);
        Main_Manager.m_CoinEvent.Invoke(player.coin);
        Saved(player);
    }

    public static void A_MinusCoin(int coin)
    {
        Player player = Loaded();
        player.coin -= coin;
        API_Game.Ins.PutCoinRequest(player.coin, null);
        Main_Manager.m_CoinEvent.Invoke(player.coin);
        Saved(player);
    }

    public static int A_Coin()
    {
        Player player = Loaded();
        return player.coin;
    }

    public static bool A_AddClothes(int id, int type)
    {
        Player player = Loaded();

        foreach (Clothes c in player.inventory.clothes)
        {
            if (c.type == type)
            {
                if (c.id == id)
                {
                    c.count++;
                    Saved(player);
                    API_Game.Ins.PostClothRequest(id, type, SaveSystem.Popupwaiii);
                    return true;
                }
            }
        }

        Clothes clothes = new Clothes();
        clothes.id = id;
        clothes.type = type;
        clothes.count++;
        player.inventory.clothes.Add(clothes);
        Saved(player);
        API_Game.Ins.PostClothRequest(id, type, SaveSystem.Popupwaiii);

        return true;
    }

    public static bool A_AddFurniture(int id, int type)
    {
        Player player = Loaded();

        Furniture furniture = new Furniture();
        furniture.id = id;
        furniture.type = type;
        player.inventory.furniture.Add(furniture);
        Saved(player);
        API_Game.Ins.PostFurnitureRequest(furniture, SaveSystem.Popupwaiii);
        return true;
    }

    public static void Popupwaiii()
    {
        Popup.Ins.PopupOne("Buy successfully", "OK", null);
    }

    public static int A_GetIDFur(Furniture f, Vector3 pos, Quaternion qua)
    {

        //Player player = Loaded();
        API_Game.Inventoryfurniture furniture;
        API_Game webg = API_Game.Ins;
        for (int i = 0; i < webg.player.player.inventoryfurniture.Count; i ++ )
        {
            furniture = webg.player.player.inventoryfurniture[i];
            if ((furniture.furnitureid == f.id && furniture.type == f.type && furniture.@using == true
                && (new Vector3(furniture.vx, furniture.vy, furniture.vz).Equals(f.position))
                && (new Quaternion(furniture.qx, furniture.qy, furniture.qz, furniture.qw).Equals(f.rotation)))
                ||
                (furniture.furnitureid == f.id && furniture.type == f.type && furniture.@using == false))
            {
                furniture.vx = pos.x;
                furniture.vy = pos.y;
                furniture.vz = pos.z;
                furniture.qx = qua.x;
                furniture.qy = qua.y;
                furniture.qz = qua.z;
                furniture.qw = qua.w;
                webg.player.player.inventoryfurniture[i] = furniture;
                return furniture.id;
            }
        }
        //try
        //{
        //    furniture = (from p in webg.player.player.inventoryfurniture
        //                 where p.furnitureid == f.id && p.type == f.type && p.@using == true
        //                 && (new Vector3 (p.vx,p.vy,p.vz).Equals(f.position)) 
        //                 && (new Quaternion (p.qx,p.qy,p.qz,p.qw).Equals(f.rotation))
        //                 select p).First();

        //    furniture.vx = pos.x;
        //    furniture.vy = pos.y;
        //    furniture.vz = pos.z;
        //    furniture.qx = qua.x;
        //    furniture.qy = qua.y;
        //    furniture.qz = qua.z;
        //    furniture.qw = qua.w;
        //    return furniture.id;
        //}
        //catch { }

        //try
        //{
        //    furniture = (from p in webg.player.player.inventoryfurniture
        //                 where p.furnitureid == f.id && p.type == f.type && p.@using == false
        //                 select p).First();
            
        //    return furniture.id;
        //}
        //catch { }
        return -1;
    }

    public static void A_SaveListFur(List<FurnitureData> furList)
    {
        if (furList == null || furList.Count == 0)
            return;
        API_Game.Ins.PutListFurnitureRequest(furList,null);
    }


    public static void A_SaveFurniture(FurnitureData data)
    {
        Player player = Loaded();
        try
        {
            Furniture furniture = (from p in player.inventory.furniture
                                   where p.type == data.type && p.furnitureIsUsing == true
                                   select p).First();
            if (data.id != furniture.id)
                furniture.furnitureIsUsing = false;
            Saved(player);
            API_Game.Ins.PutFurnitureRequest(furniture, null);
            return;
        }
        catch { }
        try
        {
            Furniture furniture = (from p in player.inventory.furniture
                                   where p.id == data.id && p.type == data.type && p.furnitureIsUsing == false
                                   select p).First();
            furniture.furnitureIsUsing = true;
            Saved(player);
            API_Game.Ins.PutFurnitureRequest(furniture, null);
            return;
        }
        catch { }
    }

    public static void A_DeleteFurniture(FurnitureData data)
    {
        Player player = Loaded();
        try
        {
            Furniture furniture = (from p in player.inventory.furniture
                                   where p.id == data.id && p.type == data.type && p.furnitureIsUsing
                                   && p.position.Equals(data.position) && p.rotation.Equals(data.rotation)
                                   select p).First();
            furniture.furnitureIsUsing = false;
            furniture.position = new Vector3(0, 0, 0);
            furniture.rotation = new Quaternion(0, 0, 0, 0);
            Saved(player);
            API_Game.Ins.PutFurnitureRequest(furniture, null);
            return;
        }
        catch { }
    }

    public static List<Furniture> A_LoadFurniture()
    {
        Player player = Loaded();
        return player.inventory.furniture;
    }

    public static bool A_AddFood(int id)
    {
        Player player = Loaded();

        foreach (Food f in player.inventory.foods)
        {
            if (f.id == id)
            {
                f.count++;
                Saved(player);
                API_Game.Ins.PostFoodRequest(f.id, SaveSystem.Popupwaiii);
                return true;
            }
        }

        Food food = new Food();
        food.id = id;
        food.count++;
        player.inventory.foods.Add(food);
        Saved(player);
        API_Game.Ins.PostFoodRequest(food.id, SaveSystem.Popupwaiii);

        return true;
    }



    public static int A_GetRealIdFood(int id)
    {
        Player player = Loaded();
        API_Game.Inventoryfood food;
        API_Game webg = API_Game.Ins;

        foreach (API_Game.Inventoryfood f in webg.player.player.inventoryfood)
        {
            if (f.foodid == id)
            {
                f.count--;
                Saved(player);
                return f.id;
            }
        }
        return -1;
    }

    public static void A_SaveListFood(Dictionary<int,int> foodDic)
    {
        API_Game.Ins.PutListFood(foodDic,null);
    }
}
