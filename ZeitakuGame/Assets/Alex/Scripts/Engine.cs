using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField]
    private Transform _leftEngine;
    [SerializeField]
    private Transform _rightEngine;

    [Header("Force Setting")]
    [SerializeField]
    private float _leftForce = 500f;
    [SerializeField]
    private float _rightForce = 500f;
    [SerializeField]
    private Vector3 _maxVelocity = new Vector3(0, 0, 20f);

    private Rigidbody _boatRB;
    private BoatController _boatController;

    [Header("Controller")]
    [SerializeField]
    [Range(0, 1f)]
    private float _controllerTiledDistance = 1f;

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

        public void Init()
        {
            isSet = false;
            originAngle = 0;
            angle = 0;
            moveDegree = 0;
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

        LeftStickControl();
        RightStickControl();
       
        if (_leftEngine == null || _rightEngine == null)
        {
            Debug.LogError("Left or Right Engine Ref is missing");
            return;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _boatRB.AddForceAtPosition(_leftEngine.forward * _leftForce * _boatRB.mass, _leftEngine.position);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _boatRB.AddForceAtPosition(_rightEngine.forward * _rightForce * _boatRB.mass, _rightEngine.position);
        }

        ForceLimit();
    }

    private void ForceLimit()
    {
        // 累計してきた力を制限制御
        _boatRB.velocity = new Vector3(
            _boatRB.velocity.x,
            _boatRB.velocity.y,
            Mathf.Clamp(_boatRB.velocity.z, -_maxVelocity.z, _maxVelocity.z));
    }

    private void RightStickControl()
    {
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

        if (_stickRight.isSet)
        {
            float oldAngle = _stickRight.angle;
            _stickRight.angle = Mathf.Atan2(_stickRight.y, _stickRight.x) * Mathf.Rad2Deg;
            float degree = Mathf.Abs(_stickRight.angle) - Mathf.Abs(oldAngle);

            if (_stickRight.angle > oldAngle)
            {
                _stickRight.moveDegree += Mathf.Abs(degree);
            }
            else if (_stickRight.angle < oldAngle)
            {
                _stickRight.moveDegree -= Mathf.Abs(degree);
            }

            if (_stickRight.moveDegree % 90 == 0)
            {
                _boatRB.AddForceAtPosition(_rightEngine.forward * _rightForce, _rightEngine.position);
                _boatRB.AddRelativeForce(Vector3.forward * _rightForce);
            }

            //if (_stickRight.moveDegree >= 300f)
            //{
            //    _boatRB.AddForceAtPosition(_rightEngine.forward * _rightForce * _boatRB.mass, _rightEngine.position);
            //    _boatRB.AddRelativeForce(Vector3.forward * _rightForce / 2f);
            //    _stickRight.moveDegree = 0;
            //}
            //else if (_stickRight.moveDegree <= -300f)
            //{
            //    _boatRB.AddForceAtPosition(_rightEngine.forward * _rightForce * _boatRB.mass, _rightEngine.position);
            //    _boatRB.AddRelativeForce(Vector3.forward * _rightForce / 2f);
            //    _stickRight.moveDegree = 0;
            //}
        }
    }

    private void LeftStickControl()
    {
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

        if (_stickLeft.isSet)
        {
            float oldAngle = _stickLeft.angle;
            _stickLeft.angle = Mathf.Atan2(_stickLeft.y, _stickLeft.x) * Mathf.Rad2Deg;
            float degree = Mathf.Abs(_stickLeft.angle) - Mathf.Abs(oldAngle);

            if (_stickLeft.angle > oldAngle)
            {
                _stickLeft.moveDegree += Mathf.Abs(degree);
            }
            else if (_stickLeft.angle < oldAngle)
            {
                _stickLeft.moveDegree -= Mathf.Abs(degree);
            }

            if(_stickLeft.moveDegree % 90 == 0)
            {
                _boatRB.AddForceAtPosition(_leftEngine.forward * _leftForce, _leftEngine.position);
                _boatRB.AddRelativeForce(Vector3.forward * _rightForce);
            }

            //if (_stickLeft.moveDegree >= 300f)
            //{
            //    _boatRB.AddForceAtPosition(_leftEngine.forward * _leftForce * _boatRB.mass, _leftEngine.position);
            //    _stickLeft.moveDegree = 0;
            //}
            //else if (_stickLeft.moveDegree <= -300f)
            //{
            //    _boatRB.AddForceAtPosition(_leftEngine.forward * _leftForce * _boatRB.mass, _leftEngine.position);
            //    _stickLeft.moveDegree = 0;
            //}
        }
    }
}
