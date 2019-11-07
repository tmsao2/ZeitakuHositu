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
        if(other.tag == "Grass")
        {
            CameraEffects.ShakeOnce(1f,2f);
            SoundManager.Instance.PlaySe("Collide");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Barrel")
        {
            CameraEffects.ShakeOnce(1f, 1f);
            SoundManager.Instance.PlaySe("Collide");
        }
        else if(collision.collider.tag == "Log")
        {
            CameraEffects.ShakeOnce(1f, 5f);
            SoundManager.Instance.PlaySe("Collide");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Grass")
        {
            _boatRB.velocity = new Vector3(0, 0, -2f);
            Debug.Log("Grass Hit !");
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
