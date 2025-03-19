using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KingTower : Tower
{
    // Start is called before the first frame update
    public bool actived;
    public GameManager manager;
    private void Start()
    {
        curHealth = maxHealth;
        actived = false;
        //manager = FindObjectOfType<GameManager>();
        Debug.Log(manager != null);
    }

    private void Update()
    {
        if (actived)
        {
            AttackNearbyUnit();
        }

        if(curHealth<=0)
        {
            manager.ClearHealth(faction);
            
        }
    }
    
}
