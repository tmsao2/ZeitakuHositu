using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidedObjectSubSpeed : MonoBehaviour
{
    private StraightMove move;
    private void Awake()
    {
        move = GetComponent<StraightMove>();
    }

    [SerializeField]
    private float decreaseRate = 2.1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SubSpeedObject")
        {
            move.Speed /= decreaseRate;
        }
    }
}
