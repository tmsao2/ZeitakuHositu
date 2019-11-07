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
    [SerializeField]
    GameObject overUI;
    [SerializeField]
    GameObject overEventSystem;

    [SerializeField]
    Engine playerBoat;

    int eventSystemTrueCount;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        eventSystemTrueCount = 100;
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
            overUI.SetActive(true);
            playerBoat.start = false;

            if (eventSystemTrueCount<0)
            {
                eventSystemTrueCount = 0;
                overEventSystem.SetActive(true);
            }
            else
            {
                eventSystemTrueCount--;
            }
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
