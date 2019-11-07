using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlane : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
    bool create;
    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        create = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -transform.lossyScale.x * 10 / 4 && !create)
        {
            Instantiate(prefab, new Vector3(transform.lossyScale.x * 10 - transform.lossyScale.x * 10 / 4 - speed, 0, 0), Quaternion.identity);
            create = true;
        }
        else if (transform.position.x < -transform.lossyScale.x * 10)
        {
            Destroy(gameObject);
        }
        transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
    }
}
