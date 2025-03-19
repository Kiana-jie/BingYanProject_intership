using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("基本参数")]
    public float time = 120f;//总时间
    public Tower[] pTowers;
    public Tower[] eTowers;
    public Transform[] enemySpawnPoint;
    private float leftTime;
    private float tc = 0; //1s计时器
    private int pHealth;
    private int eHealth;    
    public CardManager cardManager;
    public TMP_Text wintext;
    public TMP_Text clockText;

    // Start is called before the first frame update
    void Start()
    {
        wintext.gameObject.SetActive(false);
        pHealth = 0;
        foreach(var t in pTowers)
        {
            pHealth += t.curHealth;
        } 
        eHealth = pHealth;
        leftTime = time;
        UpdateTimeUI();
        StartCoroutine(EnemyAI());

    }

    IEnumerator EnemyAI()
    {
        while(true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(4f,6f));
             cardManager.PlayEnemyCard(UnityEngine.Random.Range(0, cardManager.deck.Count-1), enemySpawnPoint[UnityEngine.Random.Range(0, 2)].position, /*eTowers[2]*/transform);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        tc += Time.deltaTime;
        if(tc >= 1.0f)
        {
            tc = 0;
            leftTime--;
            UpdateTimeUI();
        }
        CheckGame();
        
    }
    private int HealthUpdate(Tower[] towers)
    {
        int tthealth = 0;
        foreach(var t in towers)
        {
            tthealth += t.curHealth;
        }
        return tthealth;
    }
    private void CheckGame()
    {
        if ( eHealth == 0 || pHealth == 0 || leftTime == 0)
        {
            EndGame();
            
        }
        pHealth = HealthUpdate(pTowers);eHealth = HealthUpdate(eTowers);
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

    public void EndGame()
    {
        wintext.gameObject.SetActive(true);
        if (eHealth <= pHealth)
        {
            wintext.text = $"Player WIN!!!";
        }
        else wintext.text = $"Enemy WIN!!!";
        
        Time.timeScale = 0;
        
    }

    public void ClearHealth(Faction fac)
    {
        Debug.Log(eHealth);
        if(fac == Faction.Player) { pHealth = 0;  }
        else { eHealth = 0; }
    }
    private void UpdateTimeUI()
    {
        if(clockText != null)
        {
            clockText.text = $"Time:{leftTime}s";
        }
    }
}   