using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : Collectible
{
    public int experience; //how much experience this item gives

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }


    protected override void CollectItem(PlayerStats player)
    {
        player.IncreaseExperience(experience);
        base.CollectItem(player);
    }
}
