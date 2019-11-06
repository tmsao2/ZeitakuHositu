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

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Grass")
        {
            _boatRB.velocity *= (1f - 0.08f);
            Debug.Log("Grass Hit !");
        }
        else if(other.tag == "Barrel")
        {
            Rigidbody otherRB = other.transform.GetComponent<Rigidbody>();
            if (otherRB == null)
            {
                Debug.LogError("Rigidbody is missing");
            }

            otherRB.velocity += _boatRB.velocity;
            _boatRB.velocity = new Vector3(0, 0, -2f);

            Debug.Log("Barrel Hit !");
        }
        else if(other.tag == "Log" || other.tag == "Shark")
        {
            _boatRB.velocity = new Vector3(0, 0, -5f);

            Debug.Log("Log Hit !");
        }
        else if(other.tag== "FloatPall")
        {
            Rigidbody otherRB = other.transform.GetComponent<Rigidbody>();
            if (otherRB == null)
            {
                Debug.LogError("Rigidbody is missing");
            }
            otherRB.velocity += _boatRB.velocity;
        }
    }
}
