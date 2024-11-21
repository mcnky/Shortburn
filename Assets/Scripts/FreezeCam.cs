using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCam : MonoBehaviour
{
    [SerializeField] private LayerMask mask;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 3, transform.forward, 3, mask);
            foreach (RaycastHit hit in hits)
            {
                hit.collider.gameObject.GetComponent<IFreezable>().Freeze();
            }
            
            
        }
    }
}
