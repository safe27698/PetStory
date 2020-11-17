using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Popup : MonoBehaviour
{

    public static Popup Ins { get { return instance; } }
    private static Popup instance;
    
    public GameObject popOne;
    public Text popOneHeadText;
    public Text popOneTextBtn;
    public UnityAction callbackOne;

    public GameObject popTwo;
    public Text popTwoHeadText;
    public Text popTwoTextBtnOne;
    public Text popTwoTextBtnTwo;
    public UnityAction<bool> callbackTwo;

    public GameObject popThree;

    public GameObject popReward;
    public Image rewardImage;
    public Text rewardText;

    public bool working;

    void Awake()
    {
        if (Popup.Ins != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
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

    public void PopupOne (string headText, string btnText , UnityAction cb)
    {
        
        popOneHeadText.text = headText;
        popOneTextBtn.text = btnText;
        callbackOne = cb;
        working = true;
        popOne.SetActive(true);
    }

    public void PopupOneOncilk ()
    {
        if(callbackOne != null)
            callbackOne();

        working = false;
        popOne.SetActive(false);
    }

    public void PopupTwo(string headText, string btnOneText, string btnTwoText, UnityAction<bool> cb)
    {

        popTwoHeadText.text = headText;
        popTwoTextBtnOne.text = btnOneText;
        popTwoTextBtnTwo.text = btnTwoText;
        callbackTwo = cb;

        working = true;
        popTwo.SetActive(true);
    }

    public void PopupTwoOncilk(bool bo)
    {
        callbackTwo(bo);

        working = false;
        popTwo.SetActive(false);
    }

    public void PopupWaiting(bool bo)
    {
        popThree.SetActive(bo);
        working = bo;
    }

    public void PopupReward(string headText, Sprite sprite, UnityAction callback)
    {
        rewardImage.sprite = sprite;
        rewardText.text = headText;
        popReward.SetActive(true);

        working = true;
        StartCoroutine(WaitForInputDown(callback));
    }

    IEnumerator WaitForInputDown(UnityAction callback)
    {
        yield return new WaitUntil(() => Input.touchCount > 0);
        working = false;
        popReward.SetActive(false);

        if(callback != null)
            callback();
    }
}
