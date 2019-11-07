using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private int _countDown;
    private bool _startFlag;

    [SerializeField]
    private Text _time;

    [SerializeField]
    private Image[] _images = new Image[4];

    private Image _currentImage;

    private void Start()
    {
        _currentImage = null;
        _countDown = 4;
        StartCoroutine("MinusTime");
        _startFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_startFlag)
        {
            if(_countDown == 4)
            {
                _time.text = "";
            }
            else
            {
                _time.text = (_countDown.ToString());
                _currentImage = _images[_countDown + 1];
            }
            
            Debug.Log(_countDown);

            if(_countDown <= 0)
            {
                _startFlag = true;
                _time.text = ("START !!!");
                Debug.Log("START !!!");
            }
        }
        else
        {
            if(_countDown <= -1)
            {
                StopCoroutine("MinusTime");
                _time.CrossFadeAlpha(0, 0.2f, false);

                if (_time.color.a == 0)
                {
                    Destroy(_time);
                }
            }
        }
    }

    IEnumerator MinusTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            _countDown--;
        }
    }
}
