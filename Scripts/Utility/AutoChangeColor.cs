using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class AutoChangeColor : MonoBehaviour
{

    public Text text;

    public float time;
    public bool bo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bo)
            time -= Time.deltaTime ;
        else
            time += Time.deltaTime ;

        text.color = Color.Lerp(Color.white, Color.black, time);

        if (time >= 1)
            bo = !bo;
        else if (time <= 0)
            bo = !bo;
    }
}
