using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private Vector3 _offset = new Vector3(0, 0, 0);

    [SerializeField]
    private Space _offsetPositionSpace = Space.Self;
    [SerializeField]
    private bool _lookAt = true;

    // Update is called once per frame
    void Update()
    {
        if(_target == null)
        {
            Debug.LogError("Target is missing");
            return;
        }

        if(_offsetPositionSpace == Space.Self)
        {
            transform.position = _target.transform.TransformPoint(_offset);
        }
        else
        {
            transform.position = _target.transform.position + _offset;
        }

        if (_lookAt)
        {
            transform.LookAt(_target.transform);
        }
        else
        {
            transform.rotation = _target.transform.rotation;
        }
    }
}
