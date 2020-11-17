using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSave : MonoBehaviour 
{
    public Player player;
}

[System.Serializable]
public class Player
{
    public string username;
    public string lastTimeInGame;
    public string lastTimeLotteyry;
    public int coin;
    public List<Pet> pets;
    public Inventory inventory;
}

[System.Serializable]
public class Pet  
{
    public int id;
    public string namePet;
    public int gender;
    public int hunger;
    public int happiness;
    public int emotion;
    public string UUID;
    public bool selected;
    public int status;
    public string lastTimeReward;
    //Pet
    public int color;
    public int head;
    public int ear;
    public int eye;
    public int eyebrow;
    public int nose;
    public int mouth;
    public int pattern;

    //Wearing
    public int accessoriesWearing;
    public int shirtWearing;
    public int pantWearing;
    public int shoeWearing;
}
[System.Serializable]
public class PetData : MonoBehaviour
{
    public int id;
    public string namePet;
    public int gender;
    public int hunger;
    public int happiness;
    public int emotion;
    public string UUID;
    public bool selected;
    public int status;
    public string lastTimeReward;
    //Pet
    public int color;
    public int head;
    public int ear;
    public int eye;
    public int eyebrow;
    public int nose;
    public int mouth;
    public int pattern;

    //Wearing
    public int accessoriesWearing;
    public int shirtWearing;
    public int pantWearing;
    public int shoeWearing;

    public void CloneData(Pet pet)
    {
        id = pet.id;
        namePet = pet.namePet;
        gender = pet.gender;
        hunger = pet.hunger;
        happiness = pet.happiness;
        emotion = pet.emotion;
        UUID = pet.UUID;
        selected = pet.selected;
        status = pet.status;
        lastTimeReward = pet.lastTimeReward;

        color = pet.color;
        head = pet.head;
        ear = pet.ear;
        eye = pet.eye;
        eyebrow = pet.eyebrow;
        nose = pet.nose;
        mouth = pet.mouth;
        pattern = pet.pattern;

        shirtWearing = pet.shirtWearing;
        pantWearing = pet.pantWearing;
        shoeWearing = pet.shoeWearing;
        accessoriesWearing = pet.accessoriesWearing;
    }
    public void ClonePetList(API_Game.PetList pet)
    {
        id = pet.gender;
        namePet = pet.name;
        gender = pet.gender;
        hunger = pet.hunger;
        happiness = pet.happiness;
       // emotion = pet.emotion;
        UUID = pet.uuid.ToString();
        selected = pet.selected;
        status = pet.status;
        lastTimeReward = pet.lastTimeReward.ToString();

        color = pet.color;
        head = pet.head;
        ear = pet.ear;
        eye = pet.eye;
        eyebrow = pet.eyebrow;
        nose = pet.nose;
        mouth = pet.mouth;
        pattern = pet.pattern;

        shirtWearing = pet.shirt;
        pantWearing = pet.pant;
        shoeWearing = pet.shoe;
        accessoriesWearing = pet.accessories;
    }
    public Pet ReturnPet (PetData pet)
    {
        Pet p = new Pet();

        p.id = pet.id;
        p.namePet = pet.namePet;
        p.gender = pet.gender;
        p.hunger = pet.hunger;
        p.happiness = pet.happiness;
        p.emotion = pet.emotion;
        p.UUID = pet.UUID;
        p.selected = pet.selected;
        p.status = pet.status;
        p.lastTimeReward = pet.lastTimeReward;

        p.color = pet.color;
        p.head = pet.head;
        p.ear = pet.ear;
        p.eye = pet.eye;
        p.eyebrow = pet.eyebrow;
        p.nose = pet.nose;
        p.mouth = pet.mouth;
        p.pattern = pet.pattern;

        p.shirtWearing = pet.shirtWearing;
        p.pantWearing = pet.pantWearing;
        p.shoeWearing = pet.shoeWearing;
        p.accessoriesWearing = pet.accessoriesWearing;

        return p;
    }
}

[System.Serializable]
public class Inventory
{
    //Owner Clothes
    public List<Clothes> clothes;

    //Owner Furniture
    public List<Furniture> furniture;

    //Owner Food
    public List<Food> foods;
}

[System.Serializable]
public class Clothes
{
    public int id;
    public int type;
    public int price;
    public int count;
}

[System.Serializable]
public class ClothesData : MonoBehaviour
{
    public int id;
    public int type;
    public int price;
    public int count;

    public void CloneData(Clothes clothes)
    {
        id = clothes.id;
        type = clothes.type;
        price = clothes.price;
        count = clothes.count;
    }
}

[System.Serializable]
public class Furniture
{
    public int id;
    public int realId;
    public int type;
    public int price;
    public bool furnitureIsUsing;
    public Vector3 position;
    public Quaternion rotation;

}

[System.Serializable]
public class FurnitureData : MonoBehaviour
{
    public int id;
    public int realId;
    public int type;
    public int price;
    public bool furnitureIsUsing;
    public Vector3 position;
    public Quaternion rotation;
    
    public void CloneData(Furniture furniture)
    {
        id = furniture.id;
        realId = furniture.realId;
        type = furniture.type;
        price = furniture.price;
        furnitureIsUsing = furniture.furnitureIsUsing;
        position = furniture.position;
        rotation = furniture.rotation;
    }

    public Furniture ReturnFurniture()
    {
        Furniture f = new Furniture();

        f.id = id;
        f.realId = realId;
        f.type = type;
        f.price = price;
        f.furnitureIsUsing = furnitureIsUsing;
        f.position = position;
        f.rotation = rotation;

        return f;
    }
}

[System.Serializable]
public class Food
{
    public int id;
    public int price;
    public int count;
}
[System.Serializable]
public class FoodData : MonoBehaviour
{
    public int id;
    public int price;
    public int count;

    public void CloneData(Food food)
    {
        id = food.id;
        price = food.price;
        count = food.count;
    }
}


public enum FurnitureType
{
    None,
    Windows,
    Decoration,
    Floor,
    Wallpaper,
    Bed,
    Toy
}

public enum ClothesType
{
    None,
    Shirts,
    Pants,
    Shoes,
    Accessories,
}

public enum FoodType
{
    None,
    Food
}

public enum BodyType
{
    None,
    Color,
    Head,
    Ear,
    Eye,
    Eyebrow,
    Nose,
    Mouth,
    Pattern
}


