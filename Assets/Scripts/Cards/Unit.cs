using System;
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
    public int health;
    public float speed;
    public float searchRange;//���з�Χ
    public float attackRange;//������Χ
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
            Unit unit = collider.GetComponent<Unit>();//��Ҫ�޳��Լ�
            
            if (unit != null && unit != this)
            {
                Debug.Log("Enemy Detected!");
                //�л�Ŀ��Ϊ����ĵ���
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
            //Ѱ·�㷨�ƶ����������
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
