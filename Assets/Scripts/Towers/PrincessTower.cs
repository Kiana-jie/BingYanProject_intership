using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessTower : MonoBehaviour
{
    public int health;
    public float attackRange;//������Χ
    public float attackRate;//����Ƶ��
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
        //��ȡ��Χ�ڵ�Target��ײ��
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
                //�л�Ŀ��Ϊ����ĵ���
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
