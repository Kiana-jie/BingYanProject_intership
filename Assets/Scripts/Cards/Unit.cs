using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum UnitType//兵种
    {
        meleeUnit,//近战
        rangedUnit//远程

    }
    public UnitType unitType;
    // Start is called before the first frame update
    public int health = 50;
    

    // Update is called once per frame
    public void TakeDammage(int damageCount)
    {
        health -= damageCount;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
