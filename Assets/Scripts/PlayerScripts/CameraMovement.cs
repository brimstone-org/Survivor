using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    [Inject]
    private PlayerStats player;
    private Transform target; 
    [SerializeField]
    public Vector3 offset = Vector3.zero;


    private void Start()
    {
        target = player.transform;
    }

    private void Update()
    {
        transform.position = target.position + offset;
    }
}
