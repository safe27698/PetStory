using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class FurnitureDaCollider : MonoBehaviour
{
    public string furnitureNameData;
    public Vector3 furniturePosData;
    public bool destroyBo;
    private Main_Manager gameManager;

    SpriteRenderer m_Sp;
    GameObject target;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<Main_Manager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && destroyBo)
        {
            FurnitureData m_Data = gameObject.GetComponent<FurnitureData>();
            FurnitureData[] furnitureData = GameObject.FindObjectsOfType<FurnitureData>();
            //FurnitureData data = (from d in furnitureData
            //                     where m_Data.id == d.id && m_Data.type == d.type 
            //                     && d.GetComponentInChildren<Text>() != null
            //                     select d).Single();

            foreach (FurnitureData d in furnitureData)
            {
                if (m_Data.id == d.id && m_Data.type == d.type && d.GetComponentInChildren<Text>() != null)
                {
                    Text t = d.GetComponentInChildren<Text>();
                    t.text = (int.Parse(t.text) + 1).ToString();
                }

            }
            m_Data.furnitureIsUsing = false;
            gameManager.furDataList.Add(m_Data);
            gameManager.idFurInstall.Remove(m_Data.realId);
           // SaveSystem.A_DeleteFurniture(m_Data);

            gameObject.transform.parent = null;
            Destroy(gameObject);

            SoundManager.Ins.audioSource.PlayOneShot(SoundManager.Ins._trash);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "DestroyZone")
        {
            m_Sp = gameObject.GetComponent<SpriteRenderer>();
            m_Sp.color = new Color (0.5f,0.5f,0.5f);
            target = gameObject;
            destroyBo = true; 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.tag == "DestroyZone")
        //{
        //    destroyBo = true;
        //    m_Sp.color = Color.red;

        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "DestroyZone")
        {
            m_Sp.color = Color.white;
            destroyBo = false;
        }
    }
}
