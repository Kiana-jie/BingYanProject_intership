using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffect : MonoBehaviour
{
    public Faction faction;
    // Start is called before the first frame update
    public float speed = 10f;
    public float damageRadius = 2f;
    public int damage = 20;
    public LayerMask enemyLayer;
    public Vector3 firePos;
    public Vector3 targetPosition;

    private void Start()
    {
        Debug.Log("Ini!");
        firePos = transform.position;
        ApplyMagicEffect();
    }

    private void ApplyMagicEffect()
    {
        float elapsedTime = 0f;
        float duration = 1f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float height = Mathf.Sin(t * Mathf.PI) * 2f;
            transform.position = Vector3.Lerp(firePos, targetPosition, t) + new Vector3(0, height, 0);
            elapsedTime += Time.deltaTime;
            
        }
        ApplyDamage();
        //Destroy(gameObject);
    }

    private void ApplyDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(targetPosition, damageRadius, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            Unit unit = enemy.GetComponent<Unit>();
            //Tower tower = enemy.GetComponent<Tower>();
            if (unit != null && unit.faction != faction)
            {
                unit.TakeDamage(damage);
            }
            /*if(tower != null && tower.faction != faction)
            {
                tower.TakeDamage(damage);   
            }*/
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
