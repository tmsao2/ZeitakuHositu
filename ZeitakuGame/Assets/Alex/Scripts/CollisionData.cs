﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CollisionData : MonoBehaviour
{
    private GamePad _gamePad;

    private Rigidbody _boatRB;

    private void Start()
    {
        _boatRB = transform.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Grass")
        {
            Vibrate(0.5f, 0.5f);
            CameraEffects.ShakeOnce(1f,2f);
            SoundManager.Instance.PlaySe("Collide");
            _boatRB.velocity = new Vector3(0, 0, -10f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Barrel")
        {
            Vibrate(0.5f, 0.5f);
            CameraEffects.ShakeOnce(1f, 1f);
            SoundManager.Instance.PlaySe("Collide");
        }
        else if(collision.collider.tag == "Log")
        {
            Vibrate(1f, 1f);
            CameraEffects.ShakeOnce(1f, 5f);
            SoundManager.Instance.PlaySe("Collide");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Grass")
        {
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

    private void Vibrate(float left,float right)
    {
        PlayerIndex player = PlayerIndex.One;
        GamePad.SetVibration(player, left, right);
        Invoke("Reset", 0.3f);
    }

    private void Reset()
    {
        PlayerIndex player = PlayerIndex.One;
        GamePad.SetVibration(player, 0, 0);
    }
}
