using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Lottery : MonoBehaviour
{
    
    public Image roll;
    public bool rollBo;
    public float speed;
    public float speedDown;

    public Sprite point10;
    public Sprite point20;
    public Sprite point50;
    public Sprite point100;
    public Sprite point200;
    public Sprite point500;
    public Sprite point800;
    public Sprite point1000;

    [Space]
    public Main_Manager manager;
    public bool working;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rollBo)
        {
            roll.transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
        }
        if (!rollBo && speedDown > 0)
        {
            roll.transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
            speed -= speedDown * Time.deltaTime;
            if (speed < 0)
                speed = 0;
        }
        
        if (!rollBo && speed == 0)
            ResultRoll();
    }

    public void OpenLottery()
    {
        if (manager.gameMode != Main_Manager.GameMode.Normal || manager.setting.working)
            return;

        SoundManager.Ins.PlaySound(SoundManager.Ins._Select);
        Player p = SaveSystem.A_LoadSaveGame();
        DateTime oldTime;
        
        if (p.lastTimeLotteyry != null && p.lastTimeLotteyry.Length > 0)
        {
            oldTime = DateTime.Parse(p.lastTimeLotteyry);
            
            if (oldTime.Day == DateTime.Now.Day)
            {
                Popup.Ins.PopupOne("You have already recieved the lucky draw for today. Please come again tomorrow.", "OK", null);
                return;
            }
        }
        
        //p.lastTimeLotteyry = DateTime.Now.ToString();
        //SaveSystem.A_SaveGame(p);

        gameObject.SetActive(true);
        working = true;
    }

    public void Roll()
    {
        if (speedDown == 0)
        {
            speedDown = UnityEngine.Random.Range(100, 200);
            rollBo = !rollBo;
            SaveSystem.A_SaveLotterry(DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }

    public void ResultRoll()
    {
        int coin = 0 ;
        Sprite s = null;
        float f = roll.transform.eulerAngles.z;
        if (f < 22 || f >= 337)
        {
            s = point10;
            coin = 10;
        }
        else if (f < 66)
        {
            s = point50;
            coin = 50;
        }
        else if (f < 111)
        {
            s = point500;
            coin = 500;
        }
        else if (f < 156)
        {
            s = point10;
            coin = 10;
        }
        else if (f < 201)
        {
            s = point100;
            coin = 100;
        }
        else if (f < 246)
        {
            s = point20;
            coin = 20;
        }
        else if (f < 291)
        {
            s = point50;
            coin = 50;
        }
        else if (f < 337)
        {
            s = point20;
            coin = 20;
        }
        SaveSystem.A_AddCoin(coin);
        Popup.Ins.PopupReward("", s, Finish);
        rollBo = !rollBo;
        speed = 500;
        speedDown = 0;
    }

    public void Finish()
    {
        working = false;
        gameObject.SetActive(false);
    }
}
