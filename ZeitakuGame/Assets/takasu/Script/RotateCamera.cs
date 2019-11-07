using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    float speed;
    private GameObject children;
    // Start is called before the first frame update
    void Start()
    {
        children = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        children.transform.LookAt(transform);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + speed, transform.eulerAngles.z);
    }
}
