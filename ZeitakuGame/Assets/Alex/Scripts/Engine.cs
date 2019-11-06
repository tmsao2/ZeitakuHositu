using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField]
    private Vector3 _maxVelocity = new Vector3(0, 0, 20f);
    private Rigidbody _boatRB;
    private BoatController _boatController;

    [Header("Controller")]
    [SerializeField]
    [Range(0, 1f)]
    private float _controllerTiledDistance = 0.5f;

    private struct Stick
    {
        // Bool For Init
        public bool isSet;

        // Angle and Degree
        public float originAngle;
        public float angle;
        public float moveDegree;

        // Basic
        public float x;
        public float y;
        public float distance;
        public float thrust;

        public void Init()
        {
            isSet = false;
            originAngle = 0;
            angle = 0;
            x = 0;
            y = 0;
            distance = 0;
        }
    }

    private Stick _stickLeft;
    private Stick _stickRight;

    // Start is called before the first frame update
    void Start()
    {
        _stickLeft.Init();
        _stickRight.Init();

        _stickLeft.moveDegree = 0;
        _stickRight.moveDegree = 0;

        _stickLeft.thrust = 0;
        _stickRight.thrust = 0;

        _boatRB = transform.GetComponent<Rigidbody>();
        _boatController = transform.GetComponent<BoatController>();
    }

    private void FixedUpdate()
    {
        _stickLeft.x = Input.GetAxis("Horizontal");
        _stickLeft.y = Input.GetAxis("Vertical");

        _stickRight.x = Input.GetAxis("Horizontal2");
        _stickRight.y = Input.GetAxis("Vertical2");

        _stickLeft.distance = Mathf.Sqrt(_stickLeft.x * _stickLeft.x + _stickLeft.y * _stickLeft.y);
        _stickRight.distance = Mathf.Sqrt(_stickRight.x * _stickRight.x + _stickRight.y * _stickRight.y);

        if (_stickRight.x != 0 || _stickRight.y != 0)
        {
            if (_stickRight.distance >= _controllerTiledDistance && !_stickRight.isSet)
            {
                _stickRight.isSet = true;
                _stickRight.originAngle = _stickRight.angle;
                _stickRight.angle = Mathf.Atan2(_stickRight.y, _stickRight.x) * Mathf.Rad2Deg;
            }
            else if (_stickRight.distance < _controllerTiledDistance)
            {
                _stickRight.Init();
            }
        }

        if (_stickLeft.x != 0 || _stickLeft.y != 0)
        {
            if (_stickLeft.distance >= _controllerTiledDistance && !_stickLeft.isSet)
            {
                _stickLeft.isSet = true;
                _stickLeft.originAngle = _stickLeft.angle;
                _stickLeft.angle = Mathf.Atan2(_stickLeft.y, _stickLeft.x) * Mathf.Rad2Deg;
            }
            else if (_stickLeft.distance < _controllerTiledDistance)
            {
                _stickLeft.Init();
            }
        }

        if(_stickLeft.isSet)
        {
            float oldAngle = _stickLeft.angle;
            _stickLeft.angle = Mathf.Atan2(_stickLeft.y, _stickLeft.x) * Mathf.Rad2Deg;
            float degree = Mathf.Abs(_stickLeft.angle) - Mathf.Abs(oldAngle);

            if (_stickLeft.angle > oldAngle)
            {
                if(_stickRight.angle == 0)
                {
                    _stickLeft.moveDegree += Mathf.Abs(degree);
                }
                _stickLeft.thrust += Mathf.Abs(degree);
            }
        }

        if(_stickRight.isSet)
        {
            float oldAngle = _stickRight.angle;
            _stickRight.angle = Mathf.Atan2(_stickRight.y, _stickRight.x) * Mathf.Rad2Deg;
            float degree = Mathf.Abs(_stickRight.angle) - Mathf.Abs(oldAngle);

           if(_stickRight.angle > oldAngle)
            {
                if(_stickLeft.angle == 0)
                {
                    _stickRight.moveDegree += Mathf.Abs(degree);
                }
                _stickRight.thrust += Mathf.Abs(degree);
            }
        }

        _stickLeft.moveDegree -= 250 * Time.deltaTime;
        _stickRight.moveDegree -= 250 * Time.deltaTime;

        _stickLeft.thrust -= 250 * Time.deltaTime;
        _stickRight.thrust -= 250 * Time.deltaTime;

        _stickLeft.moveDegree = Mathf.Clamp(_stickLeft.moveDegree, 0, 360f * 4f);
        _stickRight.moveDegree = Mathf.Clamp(_stickRight.moveDegree, 0, 360f * 4f);

        _stickLeft.thrust = Mathf.Clamp(_stickLeft.thrust, 0, 360 * 4f);
        _stickRight.thrust = Mathf.Clamp(_stickRight.thrust, 0, 360 * 4f);

        _boatRB.AddRelativeForce(Vector3.forward * (_stickRight.thrust + _stickLeft.thrust) / 180);
        transform.localEulerAngles -= new Vector3(0, _stickRight.moveDegree / (360f * 4f), 0);
        transform.localEulerAngles += new Vector3(0, _stickLeft.moveDegree / (360f * 4f), 0);

        ForceLimit();

        Debug.Log(_boatRB.velocity);
    }

    private void ForceLimit()
    {
        // 累計してきた力を制限制御
        _boatRB.velocity = new Vector3(
            Mathf.Clamp(_boatRB.velocity.x, -_maxVelocity.x, _maxVelocity.x),
            Mathf.Clamp(_boatRB.velocity.y, -_maxVelocity.y, _maxVelocity.y),
            Mathf.Clamp(_boatRB.velocity.z, -_maxVelocity.z, _maxVelocity.z));
    }
}
