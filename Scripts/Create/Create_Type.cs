using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.U2D;

public class Create_Type : MonoBehaviour
{

    public BodyType bodyType;

    public Transform parent;
    public Image item;

    public List<List<GameObject>> itemList = new List<List<GameObject>>();

    public Create_Manager cAM;
    public GameObject m_icon;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadItem();
        UpdateItem();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnEnable()
    {
        UpdateItem();

        if (m_icon != null)
            m_icon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
    }

    private void OnDisable()
    {
        if(m_icon != null)
            m_icon.transform.localScale = new Vector3(1, 1, 1);
    }

    public void LoadItem()
    {
        List<Sprite> sprites = new List<Sprite>();
        switch (bodyType)
        {
            case BodyType.Color:
                CloneItem(Atlas.Ins.GetSprites(Atlas.Ins.cha.color));
                break;

            case BodyType.Head:
                foreach (SpriteAtlas s in Atlas.Ins.chaPre.head.color)
                {
                    CloneItem(Atlas.Ins.GetSprites(s));
                }
                break;

            case BodyType.Ear:
                foreach (SpriteAtlas s in Atlas.Ins.chaPre.ear.color)
                {
                    CloneItem(Atlas.Ins.GetSprites(s));
                }
                break;

            case BodyType.Pattern:
                foreach (SpriteAtlas s in Atlas.Ins.chaPre.pattern.color)
                {
                    CloneItem(Atlas.Ins.GetSprites(s));
                }
                break;

            case BodyType.Eye:
                CloneItem(Atlas.Ins.GetSprites(Atlas.Ins.chaPre.eye));
                break;

            case BodyType.Eyebrow:
                CloneItem(Atlas.Ins.GetSprites(Atlas.Ins.chaPre.eyebrow));
                break;

            case BodyType.Mouth:
                CloneItem(Atlas.Ins.GetSprites(Atlas.Ins.chaPre.mouth));
                break;

            case BodyType.Nose:
                CloneItem(Atlas.Ins.GetSprites(Atlas.Ins.chaPre.nose));
                break;

        }
    }

    public void CloneItem (List<Sprite> sp)
    {
        itemList.Add(new List<GameObject>());
        for (int i = 0; i < sp.Count; i++)
        {
            item.sprite = sp[i];
            GameObject clone = Instantiate(item.gameObject, parent);
            clone.name = i.ToString();
            clone.SetActive(true);
            itemList[itemList.Count - 1].Add(clone);
        }
    }

    public void UpdateItem ()
    {
        if(itemList.Count > 1)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (i != cAM.color)
                {
                    for (int t = 0; t < itemList[i].Count; t++)
                    {
                        itemList[i][t].gameObject.SetActive(false);
                    }
                }
                else 
                {
                    for (int t = 0; t < itemList[i].Count; t++)
                    {
                        itemList[i][t].gameObject.SetActive(true);
                    }
                }
            }

        }
    }
}
