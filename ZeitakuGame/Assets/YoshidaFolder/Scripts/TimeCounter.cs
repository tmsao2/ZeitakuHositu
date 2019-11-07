using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public float timeRimit = 0;
    [SerializeField]
    Text text;

    public bool start;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + timeRimit;
        if(start==false)
        {
            return;
        }


        if (timeRimit <= 0)
        {
            timeRimit = 0;
            Debug.Log("timeRimit");
        }
        else
        {
            timeRimit -= Time.deltaTime;

        }
    }
    public void SetLimit(float limit)
    {
        timeRimit = limit;
    }
}
