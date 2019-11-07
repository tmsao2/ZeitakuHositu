using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResulutTimeGetter : MonoBehaviour
{
    float time = 0;
    float timeMax = 0;

    [SerializeField]
    private Text timeUi;

    [SerializeField]
    private Sprite rankB;
    [SerializeField]
    private Sprite rankA;
    [SerializeField]
    private Sprite rankS;
    [SerializeField]
    private Sprite rankSS;

    [SerializeField]
    private Image rankImage;

    // Start is called before the first frame update
    void Start()
    {
        time= TimeCounter.timeRimit;
        timeMax = TimeCounter.timeRimitMax;

        float perf= time / timeMax;

        if(perf<0.25)
        {
            rankImage .sprite= rankB;
        }
        else if(perf<0.50)
        {
            rankImage.sprite = rankA;
        }
        else if(perf<0.75)
        {
            rankImage.sprite = rankS;
        }
        else
        {
            rankImage.sprite = rankSS;
        }


    }

    // Update is called once per frame
    void Update()
    {
        timeUi.text = "" + time;

        
    }
}
