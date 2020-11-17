using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Shop_Manager : MonoBehaviour
{
    public GameObject previewObj;
    public Text cost;
    public int price;
    public Image previewImage;
    public GameObject avatarPreview;
    [Space]
    public Text playerCoin;
    public int coin;
    [Space]

    public List<GameObject> canvasMainList;
    [Space]
    public SpriteRenderer shirts;
    public SpriteRenderer pant;
    public SpriteRenderer shoe;
    public SpriteRenderer hat;
    public SpriteRenderer glass;
    public SpriteRenderer accessories;
    [Space]

    public SpriteRenderer floor;
    public SpriteRenderer ceiling;
    public SpriteRenderer wall;
    public SpriteRenderer title;
    public SpriteRenderer wallpaper;
    public SpriteRenderer accessoriesHouse;
    [Space]

    public SpriteRenderer food;
    public SpriteRenderer amulet;
    [Space]
    public GameObject parentCanvas;
    private List<GameObject> canvasList;

    private FurnitureData furnitureData;
    private ClothesData clothesData;
    private FoodData foodData;

    private void Awake()
    {
        playerCoin.text = SaveSystem.A_Coin().ToString();
        Main_Manager.m_CoinEvent.AddListener(UpdateCoin);
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasList = new List<GameObject>();
        for (int i = 0; i < parentCanvas.transform.childCount; i++)
        {
            canvasList.Add(parentCanvas.transform.GetChild(i).gameObject);
        }

//        m_Save =  SaveSystem.Loaded();
//        myCoin.text = m_Save.coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCoin(int i)
    {
        playerCoin.text = i.ToString();
    }

    public void Preview(GameObject obj)
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        furnitureData = obj.transform.parent.GetComponent<FurnitureData>();
        clothesData = obj.transform.parent.GetComponent<ClothesData>();
        foodData = obj.transform.parent.GetComponent<FoodData>();

        price = furnitureData != null ? furnitureData.price : price;
        price = clothesData != null ? clothesData.price : price;
        price = foodData != null ? foodData.price : price;
        coin = SaveSystem.A_Coin();
        previewImage.sprite = obj.GetComponent<Image>().sprite;
        cost.text = obj.transform.parent.GetChild(1).GetChild(0).GetComponent<Text>().text;
        previewObj.SetActive(true);
    }

    public void CallbackPreview(bool bo)
    {
        if (bo)
        {
            if (coin >= price)//check coin
            {
                PopupWaiting();
                avatarPreview.SetActive(false);
                previewObj.SetActive(false);
                SaveSystem.A_MinusCoin(price);

                if (furnitureData != null)
                {
                    SaveSystem.A_AddFurniture(furnitureData.id,furnitureData.type);
                    //List<Furniture> furnitures = SaveSystem.A_LoadFurniture();
                    //var furCount = from f in furnitures
                    //               where f.id == furnitureData.id && f.type == furnitureData.type
                    //               select f;
                    string ttt = furnitureData.transform.GetChild(2).GetComponent<Text>().text;
                    if (ttt == "")
                        ttt = "0";
                    int count = int.Parse(ttt);
                    count += 1;
                    furnitureData.transform.GetChild(2).GetComponent<Text>().text = count.ToString();

                    if (furnitureData.type == (int)FurnitureType.Wallpaper || furnitureData.type == (int)FurnitureType.Floor)
                    {
                        furnitureData.gameObject.GetComponentInChildren<Button>().interactable = false;
                    }

                }
                else if (clothesData != null)
                {
                    SaveSystem.A_AddClothes(clothesData.id, clothesData.type );
                    clothesData.count++;
                    clothesData.transform.GetChild(2).GetComponent<Text>().text = clothesData.count.ToString();
                }
                else if (foodData != null)
                {
                    SaveSystem.A_AddFood(foodData.id);
                    foodData.count++;
                    foodData.transform.GetChild(2).GetComponent<Text>().text = foodData.count.ToString();
                }
                else
                {

                    Debug.Log("Can not add the item");
                }
                
                furnitureData = null;
                clothesData = null;
            }
            else
            {
                Popup.Ins.PopupOne("Not enough money", "OK", null);
            }
        }
        else
        {
            avatarPreview.SetActive(false);
            previewObj.SetActive(false);
        }
    }

    public void PopupWaiting()
    {
        Popup.Ins.PopupWaiting(true);
    }
    public void SelectMainType(GameObject btn)
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        foreach (GameObject g in canvasMainList)
        {
            if (g.name == btn.name)
            {
                g.SetActive(true);
            }
            else
            {
                if (g.name != "SelectMainTypeCanvas")
                    g.SetActive(false);
            }
        }
        Debug.Log("Swich type " + btn.name);
    }
    public void SelectMainTypeSelect(Button btn)
    {
        btn.onClick.Invoke();
    }
    public void SelectType(GameObject btn)
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        foreach (GameObject g in canvasList)
        {
            if (g.name == btn.name)
            {
                g.SetActive(true);
            }
            else
            {
                if(g.name != "SelectTypeCanvas")
                     g.SetActive(false);
            }
        }
        Debug.Log("Swich type " + btn.name);
    }

    public void SelectTypeButton(GameObject btn)
    {
        GameObject selectTypeHead = btn.transform.parent.gameObject;

        for (int i = 0; i < selectTypeHead.transform.childCount; i ++)
        {
            GameObject g = selectTypeHead.transform.GetChild(i).gameObject;
            if (g.name != btn.name)
            {
                g.GetComponent<Image>().color = new Color(1,1,1,0.3f);
            }
            else
            {
                g.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }

        }
        Debug.Log("Change color " + btn.name);
    }

    public void BackToHome ()
    {
        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        PlayerPrefs.SetString("LoadScene", "Main");
        SceneManager.LoadScene("Main");
    }
    
}
