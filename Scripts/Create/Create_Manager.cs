using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using System.Linq;

public class Create_Manager : MonoBehaviour
{
    public SpriteRenderer head;
    public SpriteRenderer ear;
    public SpriteRenderer eye;
    public SpriteRenderer eyebrow;
    public SpriteRenderer nose;
    public SpriteRenderer mouth;
    public SpriteRenderer pattern;
    
    public SpriteRenderer body;
    public SpriteRenderer armLeft;
    public SpriteRenderer armRight;
    public SpriteRenderer legLeft;
    public SpriteRenderer legRight;

    public int color ;

    public List<GameObject> canvasList;

    public Button btnMale;
    public Button btnFemale;
    public Text namePet;
    

    private int numColor = 0, numHead = 0, numEar = 0, numEye = 0, numEyebrow = 0, numNose = 0, numMouth = 0, numPattern = 0;
    // Start is called before the first frame update
    void Start()
    {
        numColor = 4;
        Player player = SaveSystem.A_LoadSaveGame();

        for (int i = 0; i < player.pets.Count; i++)
        {
            if (player.pets[i].id == PlayerPrefs.GetInt("idPet"))
            {
                namePet.text = player.pets[i].namePet;
                if (player.pets[i].color < 0)
                    return;

                color = player.pets[i].color;
                head.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.head.color[color])[player.pets[i].head];
                ear.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.ear.color[color])[player.pets[i].ear];
                pattern.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.pattern.color[color])[player.pets[i].pattern];
                eye.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.eye)[player.pets[i].eye];
                eyebrow.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.eyebrow)[player.pets[i].eyebrow];
                nose.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.nose)[player.pets[i].nose];
                mouth.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.mouth)[player.pets[i].mouth];

                body.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.body)[color];
                armLeft.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.armLeft)[color];
                armRight.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.armRight)[color];
                legLeft.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.legLeft)[color];
                legRight.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.legRight)[color];

                numColor = player.pets[i].color;
                numHead = player.pets[i].head;
                numEar = player.pets[i].ear;
                numPattern = player.pets[i].pattern;
                numEye = player.pets[i].eye;
                numEyebrow = player.pets[i].eyebrow;
                numNose = player.pets[i].nose;
                numMouth = player.pets[i].mouth;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void Select_Head(GameObject obj)
    {
        int num = int.Parse(obj.name);
        head.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.head.color[color])[num];
        numHead = num;
    }
    public void Select_Ear(GameObject obj)
    {
        int num = int.Parse(obj.name);
        ear.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.ear.color[color])[num];
        numEar = num;
    }
    public void Select_Patern(GameObject obj)
    {
        int num = int.Parse(obj.name);
        pattern.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.pattern.color[color])[num];
        numPattern = num;
    }

    public void Select_Eye(GameObject obj)
    {
        int num = int.Parse(obj.name);
        eye.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.eye)[num];
        numEye = num;
    }
    public void Select_Eyebrow(GameObject obj)
    {
        int num = int.Parse(obj.name);
        eyebrow.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.eyebrow)[num];
        numEyebrow = num;
    }
    public void Select_Nose(GameObject obj)
    {
        int num = int.Parse(obj.name);
        nose.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.nose)[num];
        numNose = num;
    }
    public void Select_Mouth(GameObject obj)
    {
        int num = int.Parse(obj.name);
        mouth.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.mouth)[num];
        numMouth = num;
    }
    public void SelectGender (bool bo)
    {
        if(bo)
        {
            btnMale.interactable = false;
            btnFemale.interactable = true;
        }
        else
        {
            btnMale.interactable = true;
            btnFemale.interactable = false;
        }
    }

    public void SelectType(GameObject btn)
    {
        foreach (GameObject g in canvasList)
        {
            if (g.name == btn.name)
            {
                g.SetActive(true);
            }
            else
            {
                g.SetActive(false);
            }
        }
        Debug.Log("Swich type "+ btn.name);
    }
    
    public void SelectColor(GameObject obj)
    {
        numColor = obj.transform.GetSiblingIndex();
        numColor = numColor - 1;
        colorPick c = (colorPick)numColor;
        color = numColor;

        head.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.head.color[color])[numHead];

        ear.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.ear.color[color])[numEar];

        pattern.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.pattern.color[color])[numPattern];

        body.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.body)[numColor];
        
        armLeft.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.armLeft)[numColor];
        
        armRight.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.armRight)[numColor];
        
        legLeft.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.legLeft)[numColor];
        
        legRight.sprite = Atlas.Ins.GetSprites(Atlas.Ins.cha.legRight)[numColor];

    }

    public enum colorPick { C01, C02, C03, C04, C05, C06, C07, C08, C09, C10, C11, C12, C13, C14 }
    public void RandomSet()
    {
        colorPick colorPick = (colorPick)Random.Range(0, 13);
        numColor = (int)colorPick;
        color = numColor;

        Debug.Log("Random color " + colorPick);
        List<Sprite> spList = new List<Sprite>();
        //Head
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.head.color[color]));
        int randomPick = Random.Range(0, spList.Count);
        head.sprite = spList[randomPick];
        numHead = randomPick;
        //Ear
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.ear.color[color]));
        randomPick = Random.Range(0, spList.Count);
        ear.sprite = spList[randomPick];
        numEar = randomPick;
        //Pattern
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.pattern.color[color]));
        randomPick = Random.Range(0, spList.Count);
        pattern.sprite = spList[randomPick];
        numPattern = randomPick;
        //Eye
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.eye));
        randomPick = Random.Range(0, spList.Count);
        eye.sprite = spList[randomPick];
        numEye = randomPick;
        //Eyebrow
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.eyebrow));
        randomPick = Random.Range(0, spList.Count);
        eyebrow.sprite = spList[randomPick];
        numEyebrow = randomPick;
        //Nose
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.nose));
        randomPick = Random.Range(0, spList.Count);
        nose.sprite = spList[randomPick];
        numNose = randomPick;
        //Mouth
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.mouth));
        randomPick = Random.Range(0, spList.Count);
        mouth.sprite = spList[randomPick];
        numMouth = randomPick;
        //ArmLeft
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.armLeft));
        armLeft.sprite = spList[numColor];
        //ArmRight
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.armRight));
        armRight.sprite = spList[numColor];
        //Body
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.body));
        body.sprite = spList[numColor];
        //LegLeft
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.legLeft));
        legLeft.sprite = spList[numColor];
        //LegRight
        spList.Clear();
        spList.AddRange(Atlas.Ins.GetSprites(Atlas.Ins.cha.legRight));
        legRight.sprite = spList[numColor];

        Create_Type[] create_Type = FindObjectsOfType<Create_Type>();
        foreach (Create_Type c in create_Type)
        {
            c.UpdateItem();
        }
    }


    public void Create( )
    {
        //if (namePet.text.Length <= 0)
        //{
        //    Popup.Ins.PopupOne("Please name the pet.", "OK", null);
        //    return;
        //}

        //if ((!btnMale.interactable && !btnFemale.interactable) || (btnMale.interactable && btnFemale.interactable))
        //{
        //    Popup.Ins.PopupOne("Please select gender","OK",null);
        //    return;
        //}

        Popup.Ins.PopupWaiting(true);
        PetData pet = SaveSystem.A_LoadPet(PlayerPrefs.GetInt("idPet"));
        Debug.LogError(PlayerPrefs.GetInt("idPet"));
        pet.color = numColor;
        pet.head = numHead;
        pet.ear = numEar;
        pet.pattern = numPattern;
        pet.eye = numEye;
        pet.eyebrow = numEyebrow;
        pet.nose = numNose;
        pet.mouth = numMouth;

        //SaveSystem.A_EditPet(pet);
        API_Game.Ins.PutPetRequest(pet, FinishCrreate);
        Debug.Log("Create Saved");

        //PlayerPrefs.SetString("LoadScene", "Edit");
    }

    public void FinishCrreate()
    {
        Popup.Ins.PopupWaiting(false);
        SceneManager.LoadScene("Edit");
    }

    public void Cancel()
    {
        PlayerPrefs.SetString("LoadScene", "Edit");
        SceneManager.LoadScene("Edit");
    }
}
