using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Animation : MonoBehaviour
{

    public GEAnim test;

    private void Awake()
    {
        if (enabled)
        {
            GEAnimSystem.Instance.m_AutoAnimation = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveIn()
    {
        test.MoveIn(eGUIMove.Self);
    }
}
