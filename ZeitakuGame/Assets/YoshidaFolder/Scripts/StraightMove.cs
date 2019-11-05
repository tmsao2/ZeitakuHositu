using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        rigid.velocity = Vector3.right * speed ;

    }


}
