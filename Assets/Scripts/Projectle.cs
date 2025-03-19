using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private Unit target;
    private int damage;

    public void SetTarget(Unit newTarget, int damageAmount)
    {
        target = newTarget;
        damage = damageAmount;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
