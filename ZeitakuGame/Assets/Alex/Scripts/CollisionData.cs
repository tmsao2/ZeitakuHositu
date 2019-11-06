using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionData : MonoBehaviour
{
    private Rigidbody _boatRB;

    private void Start()
    {
        _boatRB = transform.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Grass")
        {
            _boatRB.velocity *= (1f - 0.04f);
            Debug.Log("Hit !");
        }
    }
}
