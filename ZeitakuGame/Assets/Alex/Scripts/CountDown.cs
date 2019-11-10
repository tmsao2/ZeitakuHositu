using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private int _countDown;
    private bool _startFlag;


    [SerializeField]
    private Image[] _images = new Image[4];

    private Image _currentImage;
    [SerializeField]
    Engine boatObject;
    [SerializeField]
    TimeCounter timeCounter;

    bool ennnnnd;



    private void Start()
    {
        ennnnnd = false;
        _currentImage = null;
        _countDown = 4;
        _startFlag = false;

        StartCoroutine("MinusTime");
        SoundManager.Instance.PlayBgm("Main");
    }

    // Update is called once per frame
    void Update()
    {

        if (ennnnnd == true)
        {
            return;
        }
        if (!_startFlag)
        {

            Debug.Log(_countDown);

            if (_countDown <= 0)
            {
                _startFlag = true;
                Debug.Log("START !!!");
                boatObject.start = true;
                timeCounter.start = true;
            }
        }
        else
        {
            if (_countDown <= -1)
            {
                StopCoroutine("MinusTime");
                ennnnnd = true;
            }
        }

        for (int i = 0; i < _images.Length; i++)
        {
            if (i == _countDown)
            {
                _images[i].gameObject.SetActive(true);
            }
            else
            {
                _images[i].gameObject.SetActive(false);
            }
        }
    }

    IEnumerator MinusTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            _countDown--;
        }
    }
}
