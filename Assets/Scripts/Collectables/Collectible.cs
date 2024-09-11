using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



/// <summary>
/// parrent class for the collectibles : health, ammo, exp
/// </summary>
public class Collectible : MonoBehaviour
{
   
    /// <summary>
    /// when the players enters its trigger it will fly towards the player
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            StartCoroutine(FlyTowards(collision.transform));
        }
    }


    /// <summary>
    /// move towards the player for collection
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    protected virtual IEnumerator FlyTowards(Transform targetPos)
    {
        float elapsedTime = 0;

        while (elapsedTime < 1)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos.position, (elapsedTime / 1));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos.position;
        CollectItem(targetPos.GetComponent<PlayerStats>());
    }

    protected virtual void CollectItem(PlayerStats player)
    {
        Destroy(gameObject);
    }
}
