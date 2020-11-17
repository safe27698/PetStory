using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private GameObject login;

    private void Awake()
    {
//        SaveSystem.Init();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Reset");
            SaveSystem.ResetSave();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset");
            PlayerPrefs.DeleteAll();
        }
    }

    public void StartGame_()
    {
        login.SetActive(true);
        Debug.Log("Start Game");
    }
}
