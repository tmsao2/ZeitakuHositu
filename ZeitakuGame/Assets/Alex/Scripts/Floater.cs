
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField]
    private float _amplitude = 0.015f;
    [SerializeField]
    private float _frequency = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * _frequency) * _amplitude;

        transform.position = newPos;
    }
}