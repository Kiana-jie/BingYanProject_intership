using System;
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
    public int health;
    public float speed;
    public float searchRange;//索敌范围
    public float attackRange;//攻击范围
    public int damage;
    public float attackRate;
    public LayerMask targetLayerMask;

    private float lastAttackTime;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        AttackNearestTarget();
        

    }

    private void AttackNearestTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, searchRange, targetLayerMask);
        Unit nearestUnit = null;
        //Tower targetTower = null;
        float minDistance = float.MaxValue;



        //Debug.Log(hitColliders.Length);

        foreach (Collider2D collider in hitColliders)
        {
            //Debug.Log("Enemy Detected!");
            Unit unit = collider.GetComponent<Unit>();//需要剔除自己
            
            if (unit != null && unit != this)
            {
                Debug.Log("Enemy Detected!");
                //切换目标为最近的敌人
                float distance = Vector2.Distance(transform.position, unit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestUnit = unit;
                }
                if(distance > attackRange) MoveTowards(unit.transform.position);

                else if(Time.time - lastAttackTime >= attackRate)
                {
                    unit.TakeDamage(damage);
                    lastAttackTime = Time.time;
                }
                
            }
            

        }
        if (nearestUnit == null)
        {
            Debug.Log("Start!");
            Tower tower = FindClosestTower();
            //寻路算法移动至最近的塔
            float distance = Vector2.Distance(transform.position,tower.transform.position);
            if (distance > attackRange) 
            {
                MoveTowards(tower.transform.position);
            }
            else if (Time.time - lastAttackTime >= attackRate)
            {
                tower.TakeDamage(damage);
                lastAttackTime = Time.time;
            }


        }
    }

    private Tower FindClosestTower()
    {
        Tower[] towers = FindObjectsOfType<Tower>();
        if (towers != null) Debug.Log("find tower!");
        Debug.Log(towers.Length);
        Tower closestTower = null;
        float minDistance = float.MaxValue;

        foreach (Tower tower in towers)
        {
            float distance = Vector2.Distance(transform.position, tower.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTower = tower;
            }
        }
        Debug.Log("Find Tower!");
        return closestTower;
    }

    void MoveTowards(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position,targetPos,Time.deltaTime*speed);
    }

    public void TakeDamage(int damageCount)
    {
        health -= damageCount;
        if(health <= 0)
        {
            Debug.Log("Unit Destoryed!");
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
