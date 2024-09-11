using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Collectible
{
    public int health; //how much health this item gives

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }


    protected override void CollectItem(PlayerStats player)
    {
        player.RestoreHealth(health);
        base.CollectItem(player);
    }
}