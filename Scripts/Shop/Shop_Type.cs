using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Linq;

public class Shop_Type : MonoBehaviour
{
    public string pathLoad;

    public GameObject item;
    public Shop_Manager manager;
    public Transform parent;
    public GameObject content;

    public List<List<GameObject>> itemList = new List<List<GameObject>>();
    private List<int> priceListData = new List<int>();

    private Player player;
    public ClothesType cT;
    public FurnitureType fT;
    public FoodType foodT;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start ()
    {
        player = SaveSystem.A_LoadSaveGame();
        LoadItem();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadItem()
    {
//        MySave m_Save = SaveSystem.Loaded();
        List<int> itemListData = new List<int>();

        PriceLists priceLists = Resources.Load<PriceLists>("Prefab/PriceLists");

        switch (cT)
        {
            case ClothesType.Accessories:
                priceListData.AddRange(priceLists.accessoriesPrices);
                CloneItemClothesType(Atlas.Ins.GetSprites(Atlas.Ins.clothesPre.ac), (int)ClothesType.Accessories);
                break;
            case ClothesType.Shirts:
                priceListData.AddRange(priceLists.shirtPrices);
                CloneItemClothesType(Atlas.Ins.GetSprites(Atlas.Ins.clothesPre.shirt), (int)ClothesType.Shirts);
                break;
            case ClothesType.Pants:
                priceListData.AddRange(priceLists.pantPrices);
                CloneItemClothesType(Atlas.Ins.GetSprites(Atlas.Ins.clothesPre.pant), (int)ClothesType.Pants);
                break;
            case ClothesType.Shoes:
                priceListData.AddRange(priceLists.shoePrices);
                CloneItemClothesType(Atlas.Ins.GetSprites(Atlas.Ins.clothesPre.shoe), (int)ClothesType.Shoes);
                break;
        }

        switch (fT)
        {
            case FurnitureType.Decoration:
                priceListData.AddRange(priceLists.decorationPrices);
                CloneItemFurnitureType(Atlas.Ins.GetSprites(Atlas.Ins.furPre.decoration),(int)FurnitureType.Decoration);
                break;
            case FurnitureType.Bed:
                priceListData.AddRange(priceLists.bedPrices);
                CloneItemFurnitureType(Atlas.Ins.GetSprites(Atlas.Ins.furPre.bed), (int)FurnitureType.Bed);
                break;
            case FurnitureType.Windows:
                priceListData.AddRange(priceLists.windowPrices);
                CloneItemFurnitureType(Atlas.Ins.GetSprites(Atlas.Ins.furPre.furniture), (int)FurnitureType.Windows);
           
                break;
            case FurnitureType.Floor:
                priceListData.AddRange(priceLists.floorPrices);
                CloneItemFurnitureType(Atlas.Ins.GetSprites(Atlas.Ins.furPre.floor), (int)FurnitureType.Floor);
                break;
            case FurnitureType.Wallpaper:
                priceListData.AddRange(priceLists.wallpaperPrices);
                CloneItemFurnitureType(Atlas.Ins.GetSprites(Atlas.Ins.furPre.wallpaper), (int)FurnitureType.Wallpaper);
                break;
            case FurnitureType.Toy:
                priceListData.AddRange(priceLists.toyPrices);
                CloneItemFurnitureType(Atlas.Ins.GetSprites(Atlas.Ins.furPre.other), (int)FurnitureType.Toy);
               
                break;
        }

        switch (foodT)
        {
            case FoodType.Food:
                priceListData.AddRange(priceLists.foodPrices);
                CloneItemFoodType(Atlas.Ins.GetSprites(Atlas.Ins.food.food));
                break;
        }
    }

    public void CloneItemFoodType(List<Sprite> sp)
    {
        for(int i = 0; i < sp.Count; i ++)
        {
            //SaveSystem.A_AddFood(i);
            GameObject clone = Instantiate(item.gameObject, parent);
            clone.name = i.ToString();
            FoodData data = clone.AddComponent<FoodData>();
            data.id = i;
            clone.SetActive(true);
            clone.transform.GetChild(0).GetComponent<Image>().sprite = sp[i];
            clone.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = priceListData[i].ToString();
            data.price = priceListData[i];
            for (int t = 0; t < player.inventory.foods.Count; t++)
            {
                if (player.inventory.foods[t].id == i)
                {
                    data.count = player.inventory.foods[t].count;
                }
            }

            clone.transform.GetChild(2).GetComponent<Text>().text = data.count <= 0 ? "" : data.count.ToString();
        }
    }

    public void CloneItemFurnitureType(List<Sprite> sp ,int type)
    {
        for (int i = 0; i < sp.Count; i++)
        {
            //SaveSystem.A_AddFurniture(i, type);
            GameObject clone = Instantiate(item.gameObject, parent);
            clone.name = i.ToString();
            FurnitureData data = clone.AddComponent<FurnitureData>();
            data.id = i;
            data.type = type;
            data.price = priceListData[i];
            clone.SetActive(true);
            clone.transform.GetChild(0).GetComponent<Image>().sprite = sp[i];
            clone.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = priceListData[i].ToString();

            List<Furniture> furnitures = SaveSystem.A_LoadFurniture();
            var furCount = from f in furnitures
                           where f.id == data.id && f.type == data.type
                           select f;

            clone.transform.GetChild(2).GetComponent<Text>().text = furCount.Count() <= 0 ? "" : furCount.Count().ToString();
            if(furCount.Count() >= 1 &&( type == (int)FurnitureType.Wallpaper || type == (int)FurnitureType.Floor))
            {
                clone.GetComponentInChildren<Button>().interactable = false;
            }
        }
    }

    public void CloneItemClothesType(List<Sprite> sp ,int type)
    {
        for (int i = 0; i < sp.Count; i++)
        {
            //SaveSystem.A_AddClothes(i, type);
            GameObject clone = Instantiate(item.gameObject, parent);
            clone.name = i.ToString();
            ClothesData data = clone.AddComponent<ClothesData>();
            data.id = i;
            data.type = type;
            data.price = priceListData[i];
            clone.SetActive(true);
            clone.transform.GetChild(0).GetComponent<Image>().sprite = sp[i];
            clone.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = priceListData[i].ToString();

            for (int t = 0; t < player.inventory.clothes.Count; t++)
            {
                if (player.inventory.clothes[t].type == type)
                {
                    if (player.inventory.clothes[t].id == i)
                    {
                        data.count = player.inventory.clothes[t].count;
                    }
                }
            }

            clone.transform.GetChild(2).GetComponent<Text>().text = data.count <= 0 ? "" : data.count.ToString();
        }
    }
}
