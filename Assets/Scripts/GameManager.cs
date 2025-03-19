using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("基本参数")]
    public float time = 120f;//总时间
    public Tower[] pTowers;
    public Tower[] eTowers;

    private float leftTime;
    private float tc = 0; //1s计时器
    private int pHealth;
    private int eHealth;    

    // Start is called before the first frame update
    void Start()
    {
        pHealth = 0;
        foreach(var t in pTowers)
        {
            if(t.type == Tower.TowerType.KingTower)
            {

            }
            pHealth += t.health;
        } 
        eHealth = pHealth;
        leftTime = time;
        
    }

    // Update is called once per frame
    void Update()
    {
        tc += Time.deltaTime;
        if(tc >= 1.0f)
        {
            tc = 0;
            leftTime--;
        }
        CheckGame();
        
    }
    private int HealthUpdate(Tower[] towers)
    {
        int tthealth = 0;
        foreach(var t in towers)
        {
            tthealth += t.health;
        }
        return tthealth;
    }
    private void CheckGame()
    {
        pHealth = HealthUpdate(pTowers);eHealth = HealthUpdate(eTowers);
        if ( eHealth == 0 || pHealth == 0 || leftTime == 0)
        {
            EndGame();
        }
    }

    /*private bool AllTowerDestroyed(Tower[] towers)
    {
        foreach (var t in towers)
        {
            if (t != null && t.health > 0)
            {
                return false;
            }
        }
        return true;
    }*/

    void EndGame()
    {
        if (eHealth <= pHealth) Debug.Log("Player Win!!");
        else Debug.Log("Enemy Win!!");
        Time.timeScale = 0;
    }
}
