using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Linq;

public partial class Main_Manager : MonoBehaviour
{

    float timeer = 0;
    PetAI petAI;
    Vector2 mousePos;

    public Camera interactCam;
    public GameObject interactUI;
    public GameObject interactUIBG;
    public GameObject foodCanvasList;
    public Slider happiness;
    public Slider hunger;
    public GameObject tutor3;
    public GameObject tutor4;

    public PetData petData;

    [Space]
    public Button dressUpBtn;

    private void Mode()
    {
        switch (gameMode)
        {
            case GameMode.Normal:
                Normal();
                break;
            case GameMode.Furniture:
                CustomRoom();
                break;
            case GameMode.Cloth:
                break;
            case GameMode.Interact:
                Interact();
                break;
        }
    }
    

    public void CustomRoom()
    {
        if (gameMode != GameMode.Furniture)
            return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                //เริ่มลากไอเท็ม
                case TouchPhase.Began:
                    if (Popup.Ins.working || lottery.working || setting.working)
                        return;
                    hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                    ray = Camera.main.ScreenPointToRay(touch.position);
                    if (hit2D.collider != null && target == null && hit2D.collider.GetComponent<FurnitureData>())
                    {
                        FurnitureData data = hit2D.collider.GetComponent<FurnitureData>();
                        if (data.type == (int)FurnitureType.Wallpaper || data.type == (int)FurnitureType.Floor)
                            return;
                        foreach (GameObject c in clothCanvasList)
                        {
                            if (c.activeInHierarchy)
                            {
                                CanvasGroup canvasGroup = c.GetComponentInParent<CanvasGroup>();
                                if (canvasGroup != null)
                                    canvasGroup.alpha = 0f;
                            }
                        }
                        target = hit2D.collider.gameObject;
                        target.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                        target.transform.rotation = new Quaternion(0, 0, 0, 0);
                        target.GetComponent<SpriteRenderer>().sortingOrder = 20;
                        target.GetComponent<Rigidbody2D>().gravityScale = 0;
                        target.GetComponent<PolygonCollider2D>().isTrigger = true;


                        trash.gameObject.SetActive(true);
                        trashZone.SetActive(true);
                        //  target.GetComponent<Furniture>().findSpace = true;
                    }
                    break;
                    //ลากไอเท็ม
                case TouchPhase.Moved:
                    
                    if (target != null)
                    {
                        target.SetActive(true);
                        ray = Camera.main.ScreenPointToRay(touch.position);
                        target.transform.position = new Vector2(ray.origin.x, ray.origin.y);
                    }
                  
                    break;
                case TouchPhase.Stationary:
                    break;
                    //หยุดลาก
                case TouchPhase.Ended:
                    if (target != null)
                    {
                        FurnitureData m_data = target.GetComponent<FurnitureData>();
                        m_data.furnitureIsUsing = true;
                        switch (m_data.type)
                        {
                            case (int)FurnitureType.Bed:
                                target.GetComponent<Rigidbody2D>().gravityScale = 1;
                                target.GetComponent<SpriteRenderer>().sortingOrder = 3;
                                target.layer = 15;
                                break;
                            case (int)FurnitureType.Windows:
                                SoundManager.Ins.PlaySound(SoundManager.Ins._magnet);
                                target.GetComponent<Rigidbody2D>().gravityScale = 0;
                                target.GetComponent<SpriteRenderer>().sortingOrder = 2;
                                target.layer = 16;
                                break;
                            case (int)FurnitureType.Decoration:
                                SoundManager.Ins.PlaySound(SoundManager.Ins._magnet);
                                target.GetComponent<Rigidbody2D>().gravityScale = 0;
                                target.GetComponent<SpriteRenderer>().sortingOrder = 2;
                                target.layer = 16;
                                break;
                            case (int)FurnitureType.Toy:
                                target.GetComponent<Rigidbody2D>().gravityScale = 1;
                                target.GetComponent<SpriteRenderer>().sortingOrder = 4;
                                target.layer = 17;
                                break;
                            case (int)FurnitureType.Wallpaper:
                                ChangeWallpaper(m_data);
                                target.transform.parent = null;
                                Destroy(target);
                                break;
                            case (int)FurnitureType.Floor:
                                ChangeFloor(m_data);
                                target.transform.parent = null;
                                Destroy(target);
                                break;
                        }
                        
                        foreach (GameObject c in clothCanvasList)
                        {
                            if (c.activeInHierarchy)
                            {
                                CanvasGroup canvasGroup = c.GetComponentInParent<CanvasGroup>();
                                if (canvasGroup != null)
                                    canvasGroup.alpha = 1f;
                            }
                        }
                        target.GetComponent<PolygonCollider2D>().isTrigger = false;
                        target = null;
                        trash.gameObject.SetActive(false);
                        trashZone.SetActive(false);
                    }
                    else if (target != null )
                    {
                        foreach (GameObject c in clothCanvasList)
                        {
                            if (c.activeInHierarchy)
                            {
                                CanvasGroup canvasGroup = c.GetComponentInParent<CanvasGroup>();
                                if (canvasGroup != null)
                                    canvasGroup.alpha = 1f;
                            }
                        }
                        trash.gameObject.SetActive(false);
                        trashZone.SetActive(false);
                    }
                    else if (target == null)
                    {
                        foreach (GameObject c in clothCanvasList)
                        {
                            if (c.activeInHierarchy)
                            {
                                CanvasGroup canvasGroup = c.GetComponentInParent<CanvasGroup>();
                                if (canvasGroup != null)
                                    canvasGroup.alpha = 1f;
                            }
                        }

                        trash.gameObject.SetActive(false);
                        trashZone.SetActive(false);

                    }
                    target = null;
                    break;
                case TouchPhase.Canceled:
                    break;
            }

        }
    }

    private void Normal ()
    {
        if (gameMode != GameMode.Normal)
            return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (target != null)
            {
                timeer += Time.deltaTime;
            }

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (Popup.Ins.working || lottery.working || setting.working)
                        return;
                    hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                    if (hit2D.collider != null && hit2D.collider.tag == "Pet")
                    {
                        target = hit2D.collider.gameObject;
                        petAI = target.GetComponent<PetAI>();

                    }
                    break;
                case TouchPhase.Moved:
                    if (target != null && petAI != null)
                    {
                        petAI.PetAct = PetActivity.Carry;
                        petAI.aleart = false;
                        ray = Camera.main.ScreenPointToRay(touch.position);
                        target.transform.position = new Vector2(ray.origin.x, ray.origin.y);
                    }
                    break;
                case TouchPhase.Stationary:
                    if (timeer > 0.2f && target != null && petAI != null)
                    {
                        petAI.PetAct = PetActivity.Carry;
                        petAI.aleart = false;
                        ray = Camera.main.ScreenPointToRay(touch.position);
                        target.transform.position = new Vector2(ray.origin.x, ray.origin.y);
                    }
                    break;
                case TouchPhase.Ended:
                    if (timeer < 0.2f && petAI != null)
                    {
                        petAI.rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                        petAI.aleart = false;
                        target.transform.localScale = new Vector3(0.55f, 0.55f, 1);
                        target.transform.position = new Vector3(0, -1.3f, -5);
                        target.transform.rotation = new Quaternion (0,0,0,0);
                        happiness.value = petAI.Happiness / 100;
                        hunger.value = petAI.Hunger / 100;
                        Transform[] roots = target.GetComponentsInChildren<Transform>();
                        foreach (Transform g in roots)
                            g.gameObject.layer = 21;

                        interactCam.gameObject.SetActive(true);
                        target.layer = 21;
                        interactUI.SetActive(true);
                        interactUIBG.SetActive(true);
                        foodCanvasList.SetActive(true);
                        dressUpBtn.onClick.Invoke();
                        petAI.emoHappy.SetActive(false);
                        petAI.emooHungry.SetActive(false);
                        petAI.emoHappy.layer = 10;
                        petAI.emooHungry.layer = 10;
                        gameMode = GameMode.Interact;
                        petAI.PetAct = PetActivity.Stand;
                        tutor3.SetActive(true);
                        petData = SaveSystem.A_LoadPet(petAI.id);
                        foodDic.Clear();
                    }
                    else if (target != null && petAI != null)
                    {
                        timeer = 0;
                        petAI.aleart = true;
                        petAI.PetAct = PetActivity.Stand;

                        target.transform.rotation = new Quaternion(0, 0, 0, 0);
                        target = null;
                        petAI = null;
                    }
                    break;
                case TouchPhase.Canceled:
                    break;
            }
        }
    }

    public void Interact()
    {
        if (gameMode != GameMode.Interact && petAI.PetAct != PetActivity.Stand)
            return;

        petAI.emoHappy.SetActive(false);
        petAI.emooHungry.SetActive(false);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            hit2D = Physics2D.Raycast(interactCam.ScreenToWorldPoint(touch.position), Vector2.zero);
            if (hit2D.collider != null && hit2D.collider.gameObject.GetInstanceID() == target.GetInstanceID())
            {
                if (mousePos != touch.position)
                {
                    mousePos = touch.position;
                    timeer += Time.deltaTime;
                }
                if (timeer > 0.5f)
                {
                    petAI.PetAct = PetActivity.Happy;
                    timeer = 0;
                    petAI.Happiness += 20;
                    StartCoroutine(AnimateSliderOverTime(happiness, 0.2f, 0.5f));
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                timeer = 0;
            }
        }
    }

    public void CloseInteract()
    {
        //PetData petData = SaveSystem.A_LoadPet(petAI.id);
        target.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        target.transform.position = new Vector3(0, -1.5f, 0);
        target.transform.rotation = Quaternion.identity;
        petAI.rigidbody.constraints = RigidbodyConstraints2D.None;
        petAI.PetAct = PetActivity.Stand;
        petData.hunger = (int)petAI.Hunger;
        petData.happiness = (int)petAI.Happiness;

        Transform[] roots = target.GetComponentsInChildren<Transform>();
        foreach (Transform g in roots)
            g.gameObject.layer = 10;

        interactCam.gameObject.SetActive(false);
        SaveSystem.A_EditPet(petData,true);
        if(foodDic.Count > 0)
            SaveSystem.A_SaveListFood(foodDic);
        petAI.Happiness = petAI.Happiness;
        petAI.Hunger = petAI.Hunger;
        target.layer = 10;
        timeer = 0;
        petAI.aleart = true;
        target = null;
        petAI = null;
        interactUI.SetActive(false);
        interactUIBG.SetActive(false);
        foodCanvasList.SetActive(false);
        dressUpBtn.onClick.Invoke();
        gameMode = GameMode.Normal;

    }

    public void Feeder(GameObject obj)
    {
        FoodData foodData = obj.GetComponent<FoodData>();
        if (foodData.count <= 0)
            return;

        obj.GetComponentInChildren<Text>().text = (foodData.count - 1).ToString();
        foodData.count --;
        float addFood = (10 * ((foodData.id * 0.1f) + 1)) * (foodData.id + 1);
        petAI.Hunger += addFood;
        int realIdFood = SaveSystem.A_GetRealIdFood(foodData.id);

        if (foodDic.Keys.Contains(realIdFood))
            foodDic[realIdFood] = foodData.count;
        else
            foodDic.Add(realIdFood, foodData.count);
        
        StartCoroutine(AnimateSliderOverTime(hunger, addFood * 0.01f, 0.2f));

        petAI.PetAct = PetActivity.Happy;

        if (foodData.count <= 0)
            obj.SetActive(false);
    }

    IEnumerator AnimateSliderOverTime(Slider slider, float plusValue, float seconds)
    {
        float animationTime = 0f;
        float startValue = slider.value;
        plusValue = plusValue + startValue;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            slider.value = Mathf.Lerp(startValue, plusValue, lerpValue);
            yield return null;
        }
    }

    public void SelectClothes (GameObject obj)
    {
        if (petAI == null)
            return;

        int count = int.Parse(obj.GetComponentInChildren<Text>().text);
        

        ClothesData data = obj.GetComponent<ClothesData>();
        List<Sprite> sprite = new List<Sprite>();

        //petData = SaveSystem.A_LoadPet(petAI.id);

        if (data.type == (int)ClothesType.Shirts)
        {

            if (count <= 0 && petData.shirtWearing != data.id)
                return;

            if (petData.shirtWearing != -1)
            {
                ClothesData[] clothesDatas = obj.transform.parent.GetComponentsInChildren<ClothesData>();

                foreach (ClothesData c in clothesDatas)
                {
                    if (petData.shirtWearing == c.id)
                    {
                        int cc = int.Parse(c.GetComponentInChildren<Text>().text);
                        c.GetComponentInChildren<Text>().text = (cc + 1).ToString();

                        if (petData.shirtWearing == data.id)
                        {
                            petData.shirtWearing = -1;

                            petAI.shirtBody.sprite = null;
                            petAI.shirtLeft.sprite = null;
                            petAI.shirtRight.sprite = null;
                            //SaveSystem.A_EditPet(petData, false);
                            return;
                        }
                        if (count <= 0)
                            return;
                    }
                }
            }


            petData.shirtWearing = data.id;
            sprite.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothes.shirt.number[data.id]));
            if (sprite.Count > 1)
            {
                foreach (Sprite s in sprite)
                {
                    if (s.name.ToLower().Contains("body"))
                    {
                        petAI.shirtBody.sprite = s;
                    }
                    else if (s.name.ToLower().Contains("left"))
                    {
                        petAI.shirtLeft.sprite = s;
                    }
                    else if (s.name.ToLower().Contains("right"))
                    {
                        petAI.shirtRight.sprite = s;
                    }
                }
            }
            else
            {
                petAI.shirtBody.sprite = sprite[0];
                petAI.shirtLeft.sprite = null;
                petAI.shirtRight.sprite = null;
            }

        }
        else if (data.type == (int)ClothesType.Pants)
        {
            if (count <= 0 && petData.pantWearing != data.id)
                return;
            if (petData.pantWearing != -1)
            {
                ClothesData[] clothesDatas = obj.transform.parent.GetComponentsInChildren<ClothesData>();

                foreach (ClothesData c in clothesDatas)
                {
                    if (petData.pantWearing == c.id)
                    {
                        int cc = int.Parse(c.GetComponentInChildren<Text>().text);
                        c.GetComponentInChildren<Text>().text = (cc + 1).ToString();

                        if (petData.pantWearing == data.id)
                        {
                            petData.pantWearing = -1;

                            petAI.pantBody.sprite = null;
                            petAI.pantLeft.sprite = null;
                            petAI.pantRight.sprite = null;
                            //SaveSystem.A_EditPet(petData, false);
                            return;
                        }
                        if (count < 0)
                            return;
                    }
                }
            }
            petData.pantWearing = data.id;
            sprite.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothes.pant.number[data.id]));
            if (sprite.Count > 1)
            {
                foreach (Sprite s in sprite)
                {
                    if (s.name.ToLower().Contains("body"))
                    {
                        petAI.pantBody.sprite = s;
                    }
                    else if (s.name.ToLower().Contains("left"))
                    {
                        petAI.pantLeft.sprite = s;
                    }
                    else if (s.name.ToLower().Contains("right"))
                    {
                        petAI.pantRight.sprite = s;
                    }
                }
            }
            else
            {
                petAI.pantBody.sprite = sprite[0];
                petAI.pantLeft.sprite = null;
                petAI.pantRight.sprite = null;
            }
        }
        else if (data.type == (int)ClothesType.Shoes)
        {
            if (count <= 0 && petData.shoeWearing != data.id)
                return;
            if (petData.shoeWearing != -1)
            {
                ClothesData[] clothesDatas = obj.transform.parent.GetComponentsInChildren<ClothesData>();

                foreach (ClothesData c in clothesDatas)
                {
                    if (petData.shoeWearing == c.id)
                    {
                        int cc = int.Parse(c.GetComponentInChildren<Text>().text);
                        c.GetComponentInChildren<Text>().text = (cc + 1).ToString();

                        if (petData.shoeWearing == data.id)
                        {
                            petData.shoeWearing = -1;

                            petAI.shoeLeft.sprite = null;
                            petAI.shoeRight.sprite = null;
                            //SaveSystem.A_EditPet(petData, false);
                            return;
                        }
                        if (count < 0)
                            return;
                    }
                }
            }
            petData.shoeWearing = data.id;
            sprite.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothes.shoe.number[data.id]));
            foreach (Sprite s in sprite)
            {
                if (s.name.ToLower().Contains("left"))
                {
                    petAI.shoeLeft.sprite = s;
                }
                else if (s.name.ToLower().Contains("right"))
                {
                    petAI.shoeRight.sprite = s;
                }
            }
        }
        else if (data.type == (int)ClothesType.Accessories)
        {
            if (count <= 0 && petData.accessoriesWearing != data.id)
                return;
            if (petData.accessoriesWearing != -1)
            {
                ClothesData[] clothesDatas = obj.transform.parent.GetComponentsInChildren<ClothesData>();

                foreach (ClothesData c in clothesDatas)
                {
                    if (petData.accessoriesWearing == c.id)
                    {
                        int cc = int.Parse(c.GetComponentInChildren<Text>().text);
                        c.GetComponentInChildren<Text>().text = (cc + 1).ToString();

                        if (petData.accessoriesWearing == data.id)
                        {
                            petData.accessoriesWearing = -1;

                            petAI.ac.sprite = null;
                            //SaveSystem.A_EditPet(petData, false);
                            return;
                        }
                        if (count < 0)
                            return;
                    }
                }
            }
            petData.accessoriesWearing = data.id;
            sprite.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.clothes.ac));
            petAI.ac.sprite = sprite[data.id];
        }
        obj.GetComponentInChildren<Text>().text = (count - 1).ToString();
        //SaveSystem.A_EditPet(petData,true);
    }

}
