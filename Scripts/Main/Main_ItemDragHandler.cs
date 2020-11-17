using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main_ItemDragHandler : MonoBehaviour ,IBeginDragHandler ,IDragHandler,  IEndDragHandler
{
    private Main_Manager gameManager ;
    public ScrollRect scrollRect;

    public bool _isScrolling;
    
    public FurnitureType furnitureType ;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<Main_Manager>();
    }

    public void Update()
    {

    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {

        //if (scrollRect != null && eventData.delta.y > 12)
        if (scrollRect != null && eventData.delta.y > Mathf.Abs(eventData.delta.x) + 1)
        {
            _isScrolling = false;
        }
        else
            _isScrolling = true;

        scrollRect.OnBeginDrag(eventData);
    }
    
    public void OnDrag(PointerEventData eventData)
    {

        if (_isScrolling)
            scrollRect.OnDrag(eventData);
        else
        {
            if (gameManager.target == null)
            {
                int c = int.Parse(gameObject.GetComponentInChildren<Text>().text);
                if (c <= 0)
                    return;

                CanvasGroup canvasGroup = gameObject.GetComponentInParent<CanvasGroup>();
                canvasGroup.alpha = 0f;
                
                gameManager.CloneItem(gameObject);

                gameManager.target.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                gameManager.target.transform.rotation = new Quaternion(0, 0, 0, 0);
                gameManager.target.GetComponent<SpriteRenderer>().sortingOrder = 20;
                gameManager.target.GetComponent<Rigidbody2D>().gravityScale = 0;
                gameManager.target.GetComponent<PolygonCollider2D>().isTrigger = true;
            }
        }


        //Debug.LogError(eventData.IsScrolling()); 
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //Debug.LogError("OnEndDrag");
       scrollRect.OnEndDrag(eventData);
    }
}
