using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScreen : MonoBehaviour
{
    private RectTransform rectTransform;

    private float m_Width;
    private float m_Height;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(GetComponent<RectTransform>().sizeDelta);
    }
}
