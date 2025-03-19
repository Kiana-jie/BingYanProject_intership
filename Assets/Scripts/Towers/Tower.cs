using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public enum TowerType
    {
        PrincessTower,
        KingTower,
        ProduceTower,
        WeaponTower
    }
    public Faction faction;
    public TowerType type;
    public int maxHealth;
    public int curHealth;
    public float attackRange;//������Χ
    public float attackRate;//����Ƶ��
    public int damage;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public LayerMask targetLayerMask;
    private GameManager gameManager;
    public KingTower kingTower;
    public Image healthBar;
    private float lastAttackTime;
    // Start is called before the first frame update
    private void Start()
    {
        curHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        AttackNearbyUnit();
    }

    public void AttackNearbyUnit()
    {
        
        //��ȡ��Χ�ڵ�Target��ײ��
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position,attackRange,targetLayerMask);
        //if (hitColliders.Length <= 0) return;

        Unit priorityTarget = null;
        float minDistance = float.MaxValue;


        
         
        
        foreach (Collider2D collider in hitColliders)
        {
            //Debug.Log("Enemy Detected!");
            Unit unit = collider.GetComponent<Unit>();
            if (unit != null&&unit.faction != faction)
            {
                //Debug.Log("Enemy Detected!");
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

            ShootProjectile(priorityTarget);
            lastAttackTime = Time.time;
        }

        
        
    }

    public void ShootProjectile(Unit target)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.SetTarget(target, damage);
            }
        }
    }
    public void TakeDamage(int attackForce)
    {
        curHealth -= attackForce;
        UpdateHealthUI();
        if(curHealth <= 0)
        {
            if(kingTower != null)
            kingTower.actived = true;
            //if(type==TowerType.KingTower) { gameManager.ClearHealth(faction); /*gameManager.EndGame();*/ }
            Destroy(gameObject);
            //Debug.Log("Tower Destoryed!");
        }
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)curHealth / maxHealth;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
