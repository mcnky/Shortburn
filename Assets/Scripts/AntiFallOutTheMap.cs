using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiFallOutTheMap : MonoBehaviour
{
    [SerializeField] private GameObject player; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            Vector3 newPosition = new Vector3(player.transform.position.x, 2.5f, player.transform.position.z);
            player.transform.position = newPosition;

            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}