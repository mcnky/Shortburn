using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public float blowForce = 10f; 
    public Transform player;
    
    void OnTriggerStay(Collider other)
    {
        BlowPlayerAway();
    }
    private void BlowPlayerAway()
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();

        if (playerRb != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            playerRb.AddForce(direction * -blowForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Player does not have a Rigidbody!");
        }
    }
}
