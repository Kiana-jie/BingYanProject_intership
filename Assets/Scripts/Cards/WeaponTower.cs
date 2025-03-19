using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTower : Tower
{
    private void Start()
    {
        curHealth = maxHealth;
        StartCoroutine(DieDown());
    }

    private void Update()
    {
        AttackNearbyUnit();
            
    }

    IEnumerator DieDown()
    {
        while (true) {
            yield return new WaitForSeconds(1);
            TakeDamage(5);
        }
        
    }
}
