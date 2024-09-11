using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : Collectible
{
    public int bullets; //how much ammo this item gives

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }


    protected override void CollectItem(PlayerStats player)
    {
        player.weaponController.IncreaseNumberOfProjectiles(bullets);
        base.CollectItem(player);
    }
}
