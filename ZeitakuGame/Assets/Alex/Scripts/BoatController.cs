using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField]
    private float _currentSpeed;
    [SerializeField]
    private Vector3 _lastPosition;

    [SerializeField]
    private float _thrust = 1000f;

    private Rigidbody _boatRB;

    // Start is called before the first frame update
    void Start()
    {
        _boatRB = transform.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // テストコードです。船のブースト
        if (Input.GetKeyDown(KeyCode.W))
        {
            _boatRB.AddRelativeForce(Vector3.forward * _thrust * _boatRB.mass);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _boatRB.velocity = Vector3.zero;
            _boatRB.angularVelocity = Vector3.zero;
        }

        if(Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("test");
        }

        // 現在スピードを計算して毎秒メートルです。
        _currentSpeed = (transform.position - _lastPosition).magnitude / Time.deltaTime;

        _lastPosition = transform.position;
        
        // 回転をログする
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    public float CurrentSpeed
    {
        get
        {
            return _currentSpeed;
        }
    }
}
