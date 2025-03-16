using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum UnitType//����
    {
        meleeUnit,//��ս
        rangedUnit//Զ��

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
