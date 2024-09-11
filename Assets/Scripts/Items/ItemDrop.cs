using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


/// <summary>
/// attached to the enemies, it determines what they drop
/// </summary>
public class ItemDrop : MonoBehaviour
{
    [SerializeField]
    private List<ItemSO> items;


    /// <summary>
    /// when the enemy dies
    /// </summary>
    public void OnDeath()
    {
        float rand = Random.Range(0, 50f);
        List<ItemSO> possibleDroppedItems = new List<ItemSO>(); //in case the enemy has multiple items in items and more than one matches the criteria to be dropped
                                                                //we add them to a temp list of "qualified" itmes and we later choose only one to spawn
        foreach (ItemSO item in items)
        {  
            if (rand <= item.DropRate)
            {
                possibleDroppedItems.Add(item);
            }
        }
        //drop one item 
        if (possibleDroppedItems.Count > 0)
        {
            int randomIndex = Random.Range(0, possibleDroppedItems.Count);
            Instantiate(possibleDroppedItems[randomIndex].ItemPrefab, transform.position, Quaternion.identity); //spawn the item
        }
    }
}
