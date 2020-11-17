
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CoinEvent : UnityEvent<int>{}

public partial class Main_Manager : MonoBehaviour
{
    public GameObject target;
    public RaycastHit2D hit2D;
    public Ray ray;
    public Transform roomParent;
    public GameObject furniturePrefab;
    [Space]

    public float speedCamera;
    public Main_Lottery lottery;
    public Main_Setting setting;
    [Space]

    public SpriteRenderer floor;
    public SpriteRenderer wallpaper;
    public int floorId;
    public int wallpaperId;
    [Space]
    public Image trash;
    public GameObject trashZone;

    public List<GameObject> clothCanvasList;
    public List<GameObject> furnitureCanvasList;

    public List<FurnitureData> furDataList = new List<FurnitureData>();
    public Dictionary<int, int> foodDic = new Dictionary<int, int>();

    public GameObject petPrefab;
    private Player player;
    private API_Web web;
    public Text playerNameTxt;
    public Text playerCoin;
    public RawImage profileImage;

    public static CoinEvent m_CoinEvent = new CoinEvent ();

    public List<PetAI> petList = new List<PetAI>();
    public List<int> petRewardList = new List<int>();
    public List<int> idFurInstall = new List<int>();

    private int indexReward;
    private bool getReward;

    public enum GameMode
    {
        Normal,
        Cloth,
        Furniture,
        Interact,
    }
    public GameMode gameMode;

    private void Awake()
    {
        web = GameObject.FindObjectOfType<API_Web>();
        player = SaveSystem.A_LoadSaveGame();
        LoadPet();
        playerNameTxt.text =  PlayerPrefs.GetString("slug");
        playerCoin.text = player.coin.ToString();
        StartCoroutine(GetTexture(web.m_ViewProfile.user.profile.pic, profileImage));
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Ins.PlayMusicInGame(true);
       // Calculator();
          InvokeRepeating("Calculator", 1,300);
        m_CoinEvent.AddListener(UpdateCoin);
        Popup.Ins.PopupWaiting(true);
    }

    // Update is called once per frame
    void Update()
    {
        Mode();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetReward();
            Debug.LogError("GetReward");
        }
    }

    public void UpdateCoin(int i)
    {
        playerCoin.text = i.ToString();
    }

    private void LoadPet()
    {
        for (int i = 0; i < player.pets.Count; i++)
        {
            if (player.pets[i].color < 0 || !player.pets[i].selected || player.pets[i].status < 0)
            {
                continue;
            }
            
            PetAI clone = Instantiate(petPrefab.gameObject).GetComponent<PetAI>() ;
            clone.id = player.pets[i].id;
            clone.namePet = player.pets[i].namePet;
            clone.Hunger = player.pets[i].hunger;
            clone.Happiness = player.pets[i].happiness;
            int color = player.pets[i].color;
            clone.head.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.head.color[color])[player.pets[i].head];
            clone.ear.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.ear.color[color])[player.pets[i].ear];
            clone.pattern.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.pattern.color[color])[player.pets[i].pattern];
            clone.eye.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.eye)[player.pets[i].eye];
            clone.eyebrow.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.eyebrow)[player.pets[i].eyebrow];
            clone.nose.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.nose)[player.pets[i].nose];
            clone.mouth.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.mouth)[player.pets[i].mouth];
            
            clone.body.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.body)[color];
            clone.armLeft.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.armLeft)[color];
            clone.armRight.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.armRight)[color];
            clone.legLeft.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.legLeft)[color];
            clone.legRight.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.legRight)[color];
            List<Sprite> sprite = new List<Sprite>();
            if(player.pets[i].shirtWearing >= 0)
                sprite.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothes.shirt.number[player.pets[i].shirtWearing]));
            if (sprite.Count > 1)
            {
                foreach (Sprite s in sprite)
                {
                    if (s.name.ToLower().Contains("body"))
                    {
                        clone.shirtBody.sprite = s;
                    }
                    else if (s.name.ToLower().Contains("left"))
                    {
                        clone.shirtLeft.sprite = s;
                    }
                    else if (s.name.ToLower().Contains("right"))
                    {
                        clone.shirtRight.sprite = s;
                    }
                }
            }
            else if (sprite.Count > 0)
            {
                clone.shirtBody.sprite = sprite[0];
            }
            sprite.Clear();
            if (player.pets[i].pantWearing >= 0)
                sprite.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothes.pant.number[player.pets[i].pantWearing]));
            if (sprite.Count > 1)
            {
                foreach (Sprite s in sprite)
                {
                    if (s.name.ToLower().Contains("body"))
                    {
                        clone.pantBody.sprite = s;
                    }
                    else if (s.name.ToLower().Contains("left"))
                    {
                        clone.pantLeft.sprite = s;
                    }
                    else if (s.name.ToLower().Contains("right"))
                    {
                        clone.pantRight.sprite = s;
                    }
                }
            }
            else if (sprite.Count > 0)
            {
                clone.pantBody.sprite = sprite[0];
            }

            sprite.Clear(); if (player.pets[i].shoeWearing >= 0)
                sprite.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothes.shoe.number[player.pets[i].shoeWearing]));
            foreach (Sprite s in sprite)
            {
                if (s.name.ToLower().Contains("left"))
                {
                    clone.shoeLeft.sprite = s;
                }
                else if (s.name.ToLower().Contains("right"))
                {
                    clone.shoeRight.sprite = s;
                }
            }

            sprite.Clear(); if (player.pets[i].accessoriesWearing >= 0)
                sprite.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothes.ac));
            if (sprite.Count > 0)
                clone.ac.sprite = sprite[player.pets[i].accessoriesWearing];

            petList.Add(clone);
        }

        foreach (Furniture f in player.inventory.furniture)
        {
            if (!f.furnitureIsUsing )
                continue;

            if (f.type == 3)
            {
                FurnitureData data = new FurnitureData();
                data.CloneData(f);
                ChangeFloor(data);
                continue;
            }
            if (f.type == 4)
            {
                FurnitureData data = new FurnitureData();
                data.CloneData(f);
                ChangeWallpaper(data);
                continue;
            }

            FurnitureData ff = new FurnitureData();
            ff.CloneData(f);
            CloneItem(ff);
        }
        
    }

    private void FixedUpdate()
    {

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

    public void CustomFur(GameObject obj)
    {
        if (gameMode != GameMode.Normal && gameMode != GameMode.Furniture || setting.working)
            return;
        SoundManager.Ins.PlaySound(SoundManager.Ins._Inventory);


        if (gameMode == GameMode.Furniture)
        {
            gameMode = GameMode.Normal;
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                obj.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int h = 1; h < roomParent.transform.childCount; h ++)
            {
                FurnitureData furnitureData = roomParent.GetChild(h).GetComponent<FurnitureData>();
                //furIdlist.Add(SaveSystem.A_GetIDFur(furnitureData.ReturnFurniture(), furnitureData.transform.position , furnitureData.transform.rotation)) ;
                //Debug.LogError(SaveSystem.A_GetIDFur(furnitureData.ReturnFurniture(), furnitureData.transform.position, furnitureData.transform.rotation));
            
                furnitureData.furnitureIsUsing = true;
                furnitureData.position = furnitureData.transform.position;
                furnitureData.rotation = furnitureData.transform.rotation;
                furDataList.Add(furnitureData);
            }
            SaveSystem.A_SaveListFur(furDataList);

            //  Popup.Ins.PopupWaiting(true);
        }
        else
        {
            tutor4.SetActive(true);
            obj.transform.GetChild(0).gameObject.SetActive(true);
            gameMode = GameMode.Furniture;
            furDataList.Clear();
            idFurInstall.Clear();
        }
    }

    public void CustomCloth(GameObject obj)
    {
        if (gameMode == GameMode.Interact)
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                obj.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);
            obj.transform.GetChild(obj.transform.childCount-1).gameObject.SetActive(true);
            gameMode = GameMode.Cloth;
        }
    }
    public void CloneItem(GameObject obj)
    {
        FurnitureData f = obj.GetComponent<FurnitureData>();

        Player p = SaveSystem.A_LoadSaveGame();
        GameObject clone = Instantiate(furniturePrefab, roomParent);
        FurnitureData c = clone.AddComponent<FurnitureData>() ;

        foreach (Furniture fur in p.inventory.furniture)
        {
            if (fur.id == f.id && fur.type == f.type && fur.furnitureIsUsing == false && !idFurInstall.Contains(fur.realId))
            {
                c.realId = fur.realId;
                fur.furnitureIsUsing = true;
                idFurInstall.Add(fur.realId);
                break;
            }
        }
        SaveSystem.A_SaveGame(p);
        c.id = f.id;
        c.type = f.type;
        c.furnitureIsUsing = true;
        clone.SetActive(false);
        clone.GetComponent<SpriteRenderer>().sprite = GetFurnitureSprites(f);
        clone.name = ((FurnitureType)f.type).ToString();

        PolygonCollider2D polygonCollider2D = clone.AddComponent<PolygonCollider2D>();
        polygonCollider2D.isTrigger = true;

        clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, 0);
        clone.GetComponent<Rigidbody2D>().gravityScale = 0;

        Text t = obj.GetComponentInChildren<Text>();
        t.text = (int.Parse(t.text) - 1).ToString();

        target = clone;
    }
    public void CloneItem(FurnitureData furnitureData)
    {
        FurnitureData f = furnitureData;

        GameObject clone = Instantiate(furniturePrefab, roomParent);
        FurnitureData c = clone.AddComponent<FurnitureData>();
        c.id = f.id;
        c.type = f.type;
        c.realId = f.realId;
        c.position = f.position;
        c.rotation = f.rotation;
        clone.SetActive(false);
        clone.GetComponent<SpriteRenderer>().sprite = GetFurnitureSprites(f);
        clone.name = ((FurnitureType)f.type).ToString();

        PolygonCollider2D polygonCollider2D = clone.AddComponent<PolygonCollider2D>();
        polygonCollider2D.isTrigger = true;

        clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, 0);
        clone.GetComponent<Rigidbody2D>().gravityScale = 0;


        clone.transform.position = f.position;
        clone.transform.rotation = f.rotation;

        clone.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
       // clone.transform.rotation = new Quaternion(0, 0, 0, 0);
        clone.GetComponent<SpriteRenderer>().sortingOrder = 20;
        clone.GetComponent<Rigidbody2D>().gravityScale = 0;
        clone.GetComponent<PolygonCollider2D>().isTrigger = false;

        switch (f.type)
        {
            case (int)FurnitureType.Bed:
                clone.GetComponent<Rigidbody2D>().gravityScale = 0;
                clone.GetComponent<SpriteRenderer>().sortingOrder = 3;
                clone.layer = 15;
                break;
            case (int)FurnitureType.Windows:
                clone.GetComponent<Rigidbody2D>().gravityScale = 0;
                clone.GetComponent<SpriteRenderer>().sortingOrder = 2;
                clone.layer = 16;
                break;
            case (int)FurnitureType.Decoration:
                clone.GetComponent<Rigidbody2D>().gravityScale = 0;
                clone.GetComponent<SpriteRenderer>().sortingOrder = 2;
                clone.layer = 16;
                break;
            case (int)FurnitureType.Toy:
                clone.GetComponent<Rigidbody2D>().gravityScale = 0;
                clone.GetComponent<SpriteRenderer>().sortingOrder = 4;
                clone.layer = 17;
                break;
        }

        clone.SetActive(true);

        //target = clone;
    }

    public Sprite GetFurnitureSprites(FurnitureData data)
    {
        Sprite returnSp = null;
        switch ((FurnitureType)data.type)
        {
            case FurnitureType.Bed:
                returnSp = Atlas.Ins.GetSprites(Atlas.Ins.fur.bed)[data.id];
                break;
            case FurnitureType.Decoration:
                returnSp = Atlas.Ins.GetSprites(Atlas.Ins.fur.decoration)[data.id];
                break;
            case FurnitureType.Windows:
                returnSp = Atlas.Ins.GetSprites(Atlas.Ins.fur.furniture)[data.id];
                break;
            case FurnitureType.Toy:
                returnSp = Atlas.Ins.GetSprites(Atlas.Ins.fur.other)[data.id];
                break;
            case FurnitureType.Floor:
                returnSp = Atlas.Ins.GetSprites(Atlas.Ins.furPre.floor)[data.id];
                break;
            case FurnitureType.Wallpaper:
                returnSp = Atlas.Ins.GetSprites(Atlas.Ins.furPre.wallpaper)[data.id];
                break;
        }
        return returnSp;
    }


    public void ChangeFloor(FurnitureData data)
    {
        try
        {
            FurnitureData[] furnitureData = GameObject.FindObjectsOfType<FurnitureData>();
            FurnitureData m_Data = (from d in furnitureData
                                    where floorId == d.id && (int)FurnitureType.Floor == d.type
                                    && d.GetComponentInChildren<Text>() != null
                                    select d).First();
            Text t = m_Data.GetComponentInChildren<Text>();
            t.text = (int.Parse(t.text) + 1).ToString();
            furDataList.Add(m_Data);
            //SaveSystem.A_DeleteFurniture(m_Data);
        }catch { }

        floor.sprite = Atlas.Ins.GetSprites(Atlas.Ins.fur.floor) [data.id];
        floorId = data.id;
        data.furnitureIsUsing = true;
        furDataList.Add(data);
       // API_Game.Ins.PutFurnitureRequest(data.ReturnFurniture(), null);
        //SaveSystem.A_SaveFurniture(data);
    }

    public void ChangeWallpaper(FurnitureData data)
    {
        try
        {
            FurnitureData[] furnitureData = GameObject.FindObjectsOfType<FurnitureData>();
            FurnitureData m_Data = (from d in furnitureData
                                    where wallpaperId == d.id && (int)FurnitureType.Wallpaper == d.type
                                    && d.GetComponentInChildren<Text>() != null
                                    select d).First();
            Text t = m_Data.GetComponentInChildren<Text>();
            t.text = (int.Parse(t.text) + 1).ToString();
            m_Data.furnitureIsUsing = false;
            m_Data.position = new Vector3(0, 0, 0);
            m_Data.rotation = new Quaternion(0, 0, 0, 0);

            furDataList.Add(m_Data);
            //SaveSystem.A_DeleteFurniture(m_Data);
        }
        catch { }
        wallpaper.sprite = Atlas.Ins.GetSprites(Atlas.Ins.fur.wallpaper)[data.id];
        wallpaperId = data.id;
        data.furnitureIsUsing = true;
        furDataList.Add(data);
        //API_Game.Ins.PutFurnitureRequest(data.ReturnFurniture(), null);
        //SaveSystem.A_SaveFurniture(data);
    }
   
    public void LotteryActive ()
    {
        lottery.gameObject.SetActive(true);
        lottery.rollBo = true;
        lottery.speedDown = 0;
    }

    public void GoToShop()
    {
        if (gameMode != GameMode.Normal || setting.working)
            return;

        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        PlayerPrefs.SetString("LoadScene","Shop");
        SceneManager.LoadScene("Shop");
    }

    public void GoToEdit()
    {
        if (gameMode != GameMode.Normal || setting.working)
            return;

        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        PlayerPrefs.SetString("LoadScene", "Edit");
        SceneManager.LoadScene("Edit");
    }

    public void SelectType(GameObject btn)
    {
        foreach (GameObject g in clothCanvasList)
        {
            if (g.name == btn.name)
            {
                g.SetActive(true);
            }
            else
            {
                if (g.name != "SelectMainTypeCanvas" || g.name != "TypeCanvas")
                    g.SetActive(false);
            }
        }
        Debug.Log("Swich type " + btn.name);
    }
    
    public void Calculator()
    {
        p = SaveSystem.A_LoadSaveGame();
        StartCoroutine(StatusAndRewardCalculator());
    }

    Player p ;
    PetData m_Data = new PetData();
    public IEnumerator StatusAndRewardCalculator()
    {
        DateTime oldTime;
        int totalMin = 0;
        
        if (p.lastTimeInGame != null && p.lastTimeInGame.Length > 0)
        {
            oldTime = DateTime.Parse(p.lastTimeInGame);
            totalMin = (int)DateTime.Now.Subtract(oldTime).TotalMinutes;
            string datett = DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00");
            string formm = "yyyyMMddHHmm";


        }

        p.lastTimeInGame = DateTime.Now.ToString();

        SaveSystem.A_SaveGame(p);

        for (int i = 0; i < petList.Count; i++)
        {
            m_Data  = SaveSystem.A_LoadPet(petList[i].id);

//            Debug.LogError(m_Data.UUID.Length + "**" + m_Data.UUID);
            if (m_Data.UUID.Length != 10)
                m_Data.UUID = "2019010112";

            string formm = "yyyyMMddHH";
            DateTime oldDate = DateTime.ParseExact(m_Data.UUID, formm, null);
            oldTime = oldDate;
            totalMin = (int)DateTime.Now.Subtract(oldTime).TotalMinutes;
            string newTime = DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") /*+ DateTime.Now.Minute.ToString("00")*/;
//            Debug.LogError(newTime);
            m_Data.UUID = newTime;

            if (!m_Data.selected)
                continue;

            float happy = petList[i].Happiness ;
            float hungy = petList[i].Hunger;
            float unitPerMin = 0.000115740741f;
            // float unitPerMin = 5f;


            if (Mathf.Abs(((unitPerMin * totalMin) - hungy)) < 40)
            {
                if (hungy > 40f)
                {

                    float earlierMin = (hungy - 40) / unitPerMin;
                    float afterMin = totalMin - earlierMin;

                    petList[i].Hunger -= unitPerMin * totalMin;

                    petList[i].Happiness -= (unitPerMin / 2) * earlierMin;
                    petList[i].Happiness -= ((unitPerMin / 2) * afterMin) * 2;
                }
                else
                {
                    petList[i].Hunger -= unitPerMin * totalMin;
                    petList[i].Happiness -= unitPerMin  * totalMin;
                }
            }
            else
            {
                petList[i].Hunger -= unitPerMin * totalMin;
                petList[i].Happiness -= (unitPerMin / 2) * totalMin;
            }
            m_Data.happiness = (int)petList[i].Happiness;
            m_Data.hunger = (int)petList[i].Hunger;

            
            //Reward 
            getReward = false;
            if ( m_Data.lastTimeReward.Length > 0 && petList[i].Happiness >= 50)
            {
                DateTime petDate = DateTime.Parse(m_Data.lastTimeReward);
                if (petDate.Day != DateTime.Now.Day)
                {
                    m_Data.lastTimeReward = DateTime.Now.ToString("yyyy-MM-dd");
                    RandomReward();
                    yield return null;
                    SoundManager.Ins.PlaySound(SoundManager.Ins._Reward);
                    Popup.Ins.PopupReward(m_Data.namePet+" has a gift for you.", Atlas.Ins.GetSprites(Atlas.Ins.reward.reward)[indexReward], CallbackReward);
                    
                    yield return new WaitUntil(() => getReward == true);
                }
            }
            if( m_Data.lastTimeReward.Length <= 0)
            {
                Debug.LogError("zxc");
                m_Data.lastTimeReward = DateTime.Now.ToString("yyyy-MM-dd");
                RandomReward();
                yield return null;
                SoundManager.Ins.PlaySound(SoundManager.Ins._Reward);
                Popup.Ins.PopupReward(m_Data.namePet + " has a gift for you.", Atlas.Ins.GetSprites(Atlas.Ins.reward.reward)[indexReward], CallbackReward);
                
                yield return new WaitUntil(() => getReward == true);
            }
            getReward = false;
            SaveSystem.A_EditPet(m_Data,true);

            yield return null;
        }
        
        //Daily Reward
        if (PlayerPrefs.HasKey("DailyReward"))
        {
            DateTime dailyTime = DateTime.Parse(PlayerPrefs.GetString("DailyReward"));
            if (dailyTime.Day != DateTime.Now.Day)
            {
                SaveSystem.A_AddCoin(500);
                Popup.Ins.PopupReward("", lottery.point500, null);
                PlayerPrefs.SetString("DailyReward", DateTime.Now.ToString());
            }
        }
        else
        {
            SaveSystem.A_AddCoin(500);
            Popup.Ins.PopupReward("", lottery.point500, null);
            PlayerPrefs.SetString("DailyReward", DateTime.Now.ToString());
        }
    }

    public void CallbackReward()
    {
        int coin = (indexReward + 1) * 10;
        Popup.Ins.PopupOne("You received "+coin+" coins from this gift.","OK",GetReward);
        SaveSystem.A_AddCoin(coin);
    }

    public void GetReward()
    {
        getReward = true;
    }

    public void RandomReward()
    {
        int ran = UnityEngine.Random.Range(0, 100);
        indexReward = 0;
        if (ran < 60)
        {
            indexReward = UnityEngine.Random.Range(0, Atlas.Ins.reward.reward.spriteCount / 2);
        }
        else if (ran < 95)
        {
            indexReward = UnityEngine.Random.Range(Atlas.Ins.reward.reward.spriteCount / 2, (int)(Atlas.Ins.reward.reward.spriteCount * 0.8f));
        }
        else if (ran >= 95)
        {
            indexReward = UnityEngine.Random.Range((int)(Atlas.Ins.reward.reward.spriteCount * 0.8f), Atlas.Ins.reward.reward.spriteCount);
        }
    }
}
