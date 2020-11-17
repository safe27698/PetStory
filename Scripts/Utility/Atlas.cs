using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Linq;

public class Atlas : MonoBehaviour
{

    public static Atlas Ins { get { return instance; } }
    private static Atlas instance;
                
    public AtlasChaPre      chaPre;
    public AtlasCha         cha;
    public AtlasClothesPre  clothesPre;
    public AtlasClothes     clothes;
    public AtlasFurPre      furPre;
    public AtlasFur         fur;
    public AtlasFood        food;
    public AtlasReward      reward;

    void Awake()
    {
        if (Atlas.instance != null && Atlas.instance != this)
        {
            Destroy(transform.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }
    //void OnDestroy()
    //{
    //    if (instance == this)
    //        instance = null;
    //}

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Sprite> GetSprites(SpriteAtlas spriteAtlas)
    {
        Sprite[] m_Sp;
        List<Sprite> returnList = new List<Sprite>();

        m_Sp = new Sprite[spriteAtlas.spriteCount];
        spriteAtlas.GetSprites(m_Sp);
        var sortedList = m_Sp.OrderBy(go => go.name).ToList();
        returnList.AddRange(sortedList);

        return returnList;
    }

    public Sprite GetSprites(SpriteAtlas spriteAtlas ,string name)
    {
        Sprite returnSp; 
        returnSp = spriteAtlas.GetSprite(name);
        return returnSp;
    }

}

[System.Serializable]
public class AtlasChaPre
{
    public Head head;
    [System.Serializable]
    public class Head { public List<SpriteAtlas> color; }

    public Ear ear;
    [System.Serializable]
    public class Ear { public List<SpriteAtlas> color; }

    public Pattern pattern;
    [System.Serializable]
    public class Pattern { public List<SpriteAtlas> color; }
    
    public SpriteAtlas eye;
    public SpriteAtlas eyebrow;
    public SpriteAtlas mouth;
    public SpriteAtlas nose;
}
[System.Serializable]
public class AtlasCha
{
    public Head head;
    [System.Serializable]
    public class Head { public List<SpriteAtlas> color; }

    public Ear ear;
    [System.Serializable]
    public class Ear { public List<SpriteAtlas> color; }

    public Pattern pattern;
    [System.Serializable]
    public class Pattern { public List<SpriteAtlas> color; }

    public SpriteAtlas color;

    public SpriteAtlas eye;
    public SpriteAtlas eyebrow;
    public SpriteAtlas mouth;
    public SpriteAtlas nose;

    public SpriteAtlas armLeft;
    public SpriteAtlas armRight;
    public SpriteAtlas body;
    public SpriteAtlas legLeft;
    public SpriteAtlas legRight;
}

[System.Serializable]
public class AtlasClothes
{
    public SpriteAtlas ac;


    public Shirt shirt;
    [System.Serializable]
    public class Shirt { public List<SpriteAtlas> number; }

    public Pant pant;
    [System.Serializable]
    public class Pant { public List<SpriteAtlas> number; }

    public Shoe shoe;
    [System.Serializable]
    public class Shoe { public List<SpriteAtlas> number; }
}

[System.Serializable]
public class AtlasClothesPre
{
    public SpriteAtlas ac;
    public SpriteAtlas shirt;
    public SpriteAtlas pant;
    public SpriteAtlas shoe;
}

[System.Serializable]
public class AtlasFur
{
    public SpriteAtlas bed;
    public SpriteAtlas decoration;
    public SpriteAtlas floor;
    public SpriteAtlas furniture;
    public SpriteAtlas other;
    public SpriteAtlas wallpaper;
}

[System.Serializable]
public class AtlasFurPre
{
    public SpriteAtlas bed;
    public SpriteAtlas decoration;
    public SpriteAtlas floor;
    public SpriteAtlas furniture;
    public SpriteAtlas other;
    public SpriteAtlas wallpaper;
}

[System.Serializable]
public class AtlasFood
{
    public SpriteAtlas food;
}

[System.Serializable]
public class AtlasReward
{
    public SpriteAtlas reward;
}