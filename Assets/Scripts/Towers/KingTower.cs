using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingTower : Tower
{
    // Start is called before the first frame update
    public bool actived;

    private void Start()
    {
        actived = false;
    }

    private void Update()
    {
        if (actived)
        {
            AttackNearbyUnit();
        }
    }
}
