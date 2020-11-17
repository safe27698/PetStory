using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Linq;


public class Main_Type : MonoBehaviour
{
    public string pathLoad;

    public GameObject item;
    public Transform parent;
    
    public List<Sprite> itemList;
    public FurnitureType  furnitureType;
    public ClothesType clothesType;
    public FoodType foodType;
    

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
//        m_Save = SaveSystem.Loaded();
        LoadItem();

        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
       
    }
    
    public void LoadItem()
    {

        itemList = new List<Sprite>();

        switch (pathLoad)
        {
            case "Ac":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothesPre.ac));
                break;
            case "Shirt":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothesPre.shirt));
                break;
            case "Pant":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothesPre.pant));
                break;
            case "Shoe":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothesPre.shoe));
                break;

            case "Bed":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.furPre.bed));
                break;
            case "Decoration":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.furPre.decoration));
                break;
            case "Floor":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.furPre.floor));
                break;
            case "Furniture":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.furPre.furniture));
                break;
            case "Other":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.furPre.other));
                break;
            case "Wallpaper":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.furPre.wallpaper));
                break;

            case "Food":
                itemList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.food.food));
                break;
        }

        for (int i = 0; i < itemList.Count; i++)
        {
            ClothesData c = new ClothesData();
            FurnitureData f = new FurnitureData();
            FoodData fo = new FoodData();
            Player player = SaveSystem.A_LoadSaveGame();

            item.GetComponent<Image>().sprite = itemList[i];
            GameObject clone = Instantiate(item, parent);
            clone.name = i.ToString();

            if (clothesType != ClothesType.None)
            {
                c = clone.AddComponent<ClothesData>();
                c.id = i;
            }
            else if (furnitureType != FurnitureType.None)
            {
                f = clone.AddComponent<FurnitureData>();
                f.id = i;
            }
            else if (foodType != FoodType.None)
            {
                fo = clone.AddComponent<FoodData>();
                fo.id = i;
            }
            
            switch (furnitureType)
            {
                case FurnitureType.Decoration:
                    for (int y = 0; y < player.inventory.furniture.Count; y++)
                    {
                       if (player.inventory.furniture[y].type == (int)FurnitureType.Decoration)
                        {
                            if (player.inventory.furniture[y].id == i)
                            {
                                clone.SetActive(true);
                                f.CloneData(player.inventory.furniture[y]);
                            }
                        }
                    }
                    break;
                case FurnitureType.Bed:
                    for (int y = 0; y < player.inventory.furniture.Count; y++)
                    {
                        if (player.inventory.furniture[y].type == (int)FurnitureType.Bed)
                        {
                            if (player.inventory.furniture[y].id == i)
                            {
                                clone.SetActive(true);
                                f.CloneData(player.inventory.furniture[y]);
                            }
                        }
                    }
                    break;
                case FurnitureType.Windows:
                    for (int y = 0; y < player.inventory.furniture.Count; y++)
                    {
                        if (player.inventory.furniture[y].type == (int)FurnitureType.Windows)
                        {
                            if (player.inventory.furniture[y].id == i)
                            {
                                clone.SetActive(true);
                                f.CloneData(player.inventory.furniture[y]);
                            }
                        }
                    }
                    break;
                case FurnitureType.Floor:
                    for (int y = 0; y < player.inventory.furniture.Count; y++)
                    {
                        if (player.inventory.furniture[y].type == (int)FurnitureType.Floor)
                        {
                            if (player.inventory.furniture[y].id == i)
                            {
                                clone.SetActive(true);
                                f.CloneData(player.inventory.furniture[y]);
                            }
                        }
                    }
                    break;
                case FurnitureType.Wallpaper:
                    for (int y = 0; y < player.inventory.furniture.Count; y++)
                    {
                        if (player.inventory.furniture[y].type == (int)FurnitureType.Wallpaper)
                        {
                            if (player.inventory.furniture[y].id == i)
                            {
                                clone.SetActive(true);
                                f.CloneData(player.inventory.furniture[y]);
                            }
                        }
                    }
                    break;
                case FurnitureType.Toy:
                    for (int y = 0; y < player.inventory.furniture.Count; y++)
                    {
                        if (player.inventory.furniture[y].type == (int)FurnitureType.Toy)
                        {
                            if (player.inventory.furniture[y].id == i)
                            {
                                clone.SetActive(true);
                                f.CloneData(player.inventory.furniture[y]);
                            }
                        }
                    }
                    break;
            }
            if (furnitureType != FurnitureType.None)
            {
               int t = (from tt in player.inventory.furniture
                         where f.id == tt.id && f.type == tt.type && !tt.furnitureIsUsing
                         select tt).Count();

                Text txt = clone.GetComponentInChildren<Text>();

                if (t > 0)
                    txt.text = t.ToString();
                else
                    txt.text = "0";
            }

            switch (clothesType)
            {
                case ClothesType.Accessories:
                    for (int y = 0; y < player.inventory.clothes.Count; y++)
                    {
                        if (player.inventory.clothes[y].type == (int)ClothesType.Accessories)
                        {
                            if (player.inventory.clothes[y].id == i)
                            {
                                clone.SetActive(true);
                                c.CloneData(player.inventory.clothes[y]);
                            }
                        }
                    }
                    break;
                case ClothesType.Shirts:
                    for (int y = 0; y < player.inventory.clothes.Count; y++)
                    {
                        if (player.inventory.clothes[y].type == (int)ClothesType.Shirts)
                        {
                            if (player.inventory.clothes[y].id == i)
                            {
                                clone.SetActive(true);
                                c.CloneData(player.inventory.clothes[y]);
                            }
                        }
                    }
                    break;
                case ClothesType.Pants:
                    for (int y = 0; y < player.inventory.clothes.Count; y++)
                    {
                        if (player.inventory.clothes[y].type == (int)ClothesType.Pants)
                        {
                            if (player.inventory.clothes[y].id == i)
                            {
                                clone.SetActive(true);
                                c.CloneData(player.inventory.clothes[y]);
                            }
                        }
                    }
                    break;
                case ClothesType.Shoes:
                    for (int y = 0; y < player.inventory.clothes.Count; y++)
                    {
                        if (player.inventory.clothes[y].type == (int)ClothesType.Shoes)
                        {
                            if (player.inventory.clothes[y].id == i)
                            {
                                clone.SetActive(true);
                                c.CloneData(player.inventory.clothes[y]);
                            }
                        }
                    }
                    break;
            }

            if (clothesType != ClothesType.None)
            {
                try
                {
                    Clothes t = (from tt in player.inventory.clothes
                                 where c.id == tt.id && c.type == tt.type
                                 select tt).First();
                    
                    Text txt = clone.GetComponentInChildren<Text>();

                    if (t.count > 0)
                    {
                        txt.text = t.count.ToString();

                        foreach (Pet p in player.pets)
                        {
                            if (clothesType == ClothesType.Shirts && p.shirtWearing == c.id)
                            {
                                txt.text = (int.Parse(txt.text)-1).ToString();
                            }
                            else if (clothesType == ClothesType.Pants && p.pantWearing == c.id)
                            {
                                txt.text = (int.Parse(txt.text) - 1).ToString();
                            }
                            else if (clothesType == ClothesType.Shoes && p.shoeWearing == c.id)
                            {
                                txt.text = (int.Parse(txt.text) - 1).ToString();
                            }
                            else if (clothesType == ClothesType.Accessories && p.accessoriesWearing == c.id)
                            {
                                txt.text = (int.Parse(txt.text) - 1).ToString();
                            }
                        }
                    }
                    else
                        txt.text = "0";
                }
                catch { }
            }
            switch (foodType)
            {
                case FoodType.Food:
                    for (int y = 0; y < player.inventory.foods.Count; y++)
                    {
                        if (player.inventory.foods[y].id == i && player.inventory.foods[y].count > 0)
                        {
                            clone.SetActive(true);
                            fo.CloneData(player.inventory.foods[y]);
                        }
                    }
                    break;
            }

            if (foodType != FoodType.None)
            {
                try
                {
                    Food t = (from tt in player.inventory.foods
                              where fo.id == tt.id
                              select tt).First();
                    Text txt = clone.GetComponentInChildren<Text>();

                    if (t.count > 0)
                        txt.text = t.count.ToString();
                    else
                        txt.text = "0";
                }
                catch { }
            }

        }
    }
}
