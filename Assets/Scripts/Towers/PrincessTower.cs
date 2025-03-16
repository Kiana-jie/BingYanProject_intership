using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessTower : MonoBehaviour
{
    public int health;
    public float attackRange;//攻击范围
    public float attackRate;//攻击频率
    public int damage;
    public LayerMask targetLayerMask;

    private float lastAttackTime;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        AttackNearbyUnit();
    }

    private void AttackNearbyUnit()
    {
        //Debug.Log("Start!");
        //获取范围内的Target碰撞体
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position,attackRange,targetLayerMask);
        //if (hitColliders.Length <= 0) return;

        Unit priorityTarget = null;
        float minDistance = float.MaxValue;


        
        Debug.Log(hitColliders.Length);
        
        foreach (Collider2D collider in hitColliders)
        {
            //Debug.Log("Enemy Detected!");
            Unit unit = collider.GetComponent<Unit>();
            if (unit != null)
            {
                Debug.Log("Enemy Detected!");
                //切换目标为最近的敌人
                float distance = Vector2.Distance(transform.position, unit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    priorityTarget = unit;
                }
            }
            
        }

        if (priorityTarget != null && Time.time - lastAttackTime >= attackRate) 
        {
            priorityTarget.TakeDammage(damage);
            lastAttackTime = Time.time;
        }

        
        
    }
    public void TakeDammage(int attackForce)
    {
        health -= attackForce;
        if(health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Tower Destoryed!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
