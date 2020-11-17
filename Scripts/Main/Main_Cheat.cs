using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main_Cheat : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private void Start()
    {

    }

    public void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.LogError("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.LogError("OnDrag"); 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SaveSystem.A_AddCoin(5000);
    }
}
