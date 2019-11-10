using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

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

    private bool _changeSpeed;
    [SerializeField]
    [Range(0, 0.1f)]
    private float _offsetSpeed;

    [SerializeField]
    private GameObject fakeL;
    [SerializeField]
    private GameObject fakeR;

    [SerializeField]
    private Transform _waterLevel;

    [SerializeField]
    private GameObject _boat;

    [SerializeField]
    private ParticleSystem _splashL;
    [SerializeField]
    private ParticleSystem _splashR;
    [SerializeField]
    private ParticleSystem _splashFL;
    [SerializeField]
    private ParticleSystem _spalshFR;

    public bool start;



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
        public float movement;

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

    private float _frontTurn;
    private float _turnSpeed;
    private int _reached;

    PlayerIndex player;
    GamePadState state;
    private bool _oldFlag;
    private bool _newFlag;
    private bool _vibrateFlag;

    public bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerIndex.One;
        start = false;

        _stickLeft.Init();
        _stickRight.Init();

        _stickLeft.moveDegree = 0;
        _stickRight.moveDegree = 0;

        _stickLeft.thrust = 0;
        _stickRight.thrust = 0;

        _stickLeft.movement = 0;
        _stickRight.movement = 0;

        _boatRB = transform.GetComponent<Rigidbody>();
        _boatController = transform.GetComponent<BoatController>();

        _changeSpeed = false;
        //_offsetSpeed = 0.01f;

        _turnSpeed = 0;
        _reached = 0;
        _vibrateFlag = false;
        _oldFlag = false;
        _newFlag = false;
        isHit = false;

        if (fakeL == null || fakeR == null)
        {
            Debug.Log("Fake is missing");
        }
    }

    private void FixedUpdate()
    {


        state = GamePad.GetState(player);
        _oldFlag = _newFlag;
        _newFlag = (state.Buttons.Y == ButtonState.Pressed);

        if (start)
        {
            _stickLeft.x = Input.GetAxis("Horizontal");
            _stickLeft.y = Input.GetAxis("Vertical");

            _stickRight.x = Input.GetAxis("Horizontal2");
            _stickRight.y = Input.GetAxis("Vertical2");
        }

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

        if (_stickLeft.isSet)
        {
            float oldAngle = _stickLeft.angle;
            _stickLeft.angle = Mathf.Atan2(_stickLeft.y, _stickLeft.x) * Mathf.Rad2Deg;
            float degree = Mathf.Abs(_stickLeft.angle) - Mathf.Abs(oldAngle);

            if (_stickLeft.angle > oldAngle || _stickLeft.angle < oldAngle)
            {
                if (_stickRight.distance < _controllerTiledDistance)
                {
                    _stickLeft.moveDegree += Mathf.Abs(degree);
                }
                else
                {
                    _stickLeft.thrust += Mathf.Abs(degree);
                }
                _stickLeft.movement += Mathf.Abs(degree);
            }
        }

        if (_stickRight.isSet)
        {
            float oldAngle = _stickRight.angle;
            _stickRight.angle = Mathf.Atan2(_stickRight.y, _stickRight.x) * Mathf.Rad2Deg;
            float degree = Mathf.Abs(_stickRight.angle) - Mathf.Abs(oldAngle);

            if (_stickRight.angle > oldAngle || _stickRight.angle < oldAngle)
            {
                if (_stickLeft.distance < _controllerTiledDistance)
                {
                    _stickRight.moveDegree += Mathf.Abs(degree);
                }
                else
                {
                    _stickRight.thrust += Mathf.Abs(degree);
                }
                _stickRight.movement += Mathf.Abs(degree);
            }
        }

        _stickLeft.moveDegree -= 250f * Time.deltaTime;
        _stickRight.moveDegree -= 250f * Time.deltaTime;

        float minus = 200f;
        _stickLeft.thrust -= minus * Time.deltaTime;
        _stickRight.thrust -= minus * Time.deltaTime;

        _stickLeft.movement -= 250f * Time.deltaTime;
        _stickRight.movement -= 250f * Time.deltaTime;

        _stickLeft.moveDegree = Mathf.Clamp(_stickLeft.moveDegree, 0, 360f * 4f);
        _stickRight.moveDegree = Mathf.Clamp(_stickRight.moveDegree, 0, 360f * 4f);

        _stickLeft.thrust = Mathf.Clamp(_stickLeft.thrust, 0.0f, 360 * 4f);
        _stickRight.thrust = Mathf.Clamp(_stickRight.thrust, 0.0f, 360 * 4f);

        _stickLeft.movement = Mathf.Clamp(_stickLeft.movement, 0.0f, 360 * 4f);
        _stickRight.movement = Mathf.Clamp(_stickRight.movement, 0.0f, 360 * 4f);

        float bindThrust = (_stickLeft.thrust + _stickRight.thrust) / 360f * 4f;
        if (start)
        {
            transform.Translate(new Vector3(0, 0, bindThrust * (0.2f + _offsetSpeed) * Time.deltaTime));

            transform.localEulerAngles -= new Vector3(0, _stickRight.moveDegree / (1440f / 2f), 0);
            transform.localEulerAngles += new Vector3(0, _stickLeft.moveDegree / (1440f / 2f), 0);
        }

        // Rotation for turning
        float turn = (_stickRight.moveDegree - _stickLeft.moveDegree) / 72f;
        turn = Mathf.Clamp(turn, -20f, 20f);

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            turn);

        if (_reached == 0)
        {
            _turnSpeed += (_stickLeft.thrust + _stickRight.thrust) / 7200f;
            if (_turnSpeed >= 15f)
            {
                _reached = 1;
            }
            else
            {
                _turnSpeed -= 0.1f;
                _turnSpeed = Mathf.Clamp(_turnSpeed, 0, 15f);
            }
        }
        else if (_reached == 1)
        {
            _turnSpeed -= 0.25f;
            if (_turnSpeed <= 10f)
            {
                _reached = 0;
            }
        }
        else if (_reached == 2)
        {
            _turnSpeed -= 0.25f;
            _turnSpeed = Mathf.Clamp(_turnSpeed, 0, 15f);
        }

        // Rotation for boosting
        //_frontTurn -= 1f;
        //_frontTurn += (_stickLeft.thrust + _stickRight.thrust) / 1440f;
        //_frontTurn = Mathf.Clamp(_frontTurn, 0f, 15f);

        _boat.transform.localEulerAngles = new Vector3(
            _turnSpeed,
            _boat.transform.localEulerAngles.y,
            _boat.transform.localEulerAngles.z);

        // Particle emission
        var l = _splashL.emission;
        var r = _splashR.emission;
        l.rateOverTime = 70f * (_stickLeft.movement / 1440f);
        r.rateOverTime = 70f * (_stickRight.movement / 1440f);

        var lf = _splashFL.emission;
        var rf = _spalshFR.emission;
        lf.rateOverTime = 70f * (_stickLeft.movement + _stickRight.movement) / 2880f;
        rf.rateOverTime = 70f * (_stickLeft.movement + _stickRight.movement) / 2880f;

        ForceLimit();

        // Propeller Rotation
        float rotationL = (_stickLeft.moveDegree + _stickLeft.thrust) / 45f;
        float rotationR = (_stickRight.moveDegree + _stickRight.thrust) / 45f;
        fakeL.transform.eulerAngles -= new Vector3(0, 0, rotationL);
        fakeR.transform.eulerAngles += new Vector3(0, 0, rotationR);

        float vibL = (_stickLeft.moveDegree + _stickLeft.thrust) / 28800f;
        float vibR = (_stickRight.moveDegree + _stickRight.thrust) / 28800f;

        if (_newFlag && !_oldFlag)
        {
            _vibrateFlag = !_vibrateFlag;
            if (!_vibrateFlag)
            {
                GamePad.SetVibration(player, 0, 0);
            }
        }

        if (_vibrateFlag && !isHit)
        {
            GamePad.SetVibration(player, 0, vibL + vibR);
        }

        Debug.Log(_vibrateFlag);

        //transform.Translate(new Vector3(0,-0.1f,0));
        //if (transform.position.y <= _waterLevel.position.y + 0.002f)
        //{
        //    transform.position = new Vector3(transform.position.x, _waterLevel.position.y + 0.002f, transform.position.z);
        //}
    }

    private void ForceLimit()
    {
        // 累計してきた力を制限制御
        _boatRB.velocity = new Vector3(
            Mathf.Clamp(_boatRB.velocity.x, -_maxVelocity.x, _maxVelocity.x),
            Mathf.Clamp(_boatRB.velocity.y, -_maxVelocity.y, _maxVelocity.y),
            Mathf.Clamp(_boatRB.velocity.z, -_maxVelocity.z, _maxVelocity.z));
    }

    private void OnApplicationQuit()
    {
        GamePad.SetVibration(player, 0, 0);
    }
}
