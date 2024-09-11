using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    private EnemyStats enemyStats;
    Transform player;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    public void InitializeMovement( Transform target)
    {
        player = target;
    }


    private void Update()
    {
        if (player!=null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.CurrentMoveSpeed * Time.deltaTime);
            FacePlayer();
        }
         
    }

    void FacePlayer()
    {
        if (transform.localScale.x < 0 && transform.position.x > player.position.x)
        {
            transform.localScale =  new Vector3((-1) * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (transform.localScale.x > 0 && transform.position.x < player.position.x)
        {
            transform.localScale = new Vector3((-1)* transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    
    }
}
