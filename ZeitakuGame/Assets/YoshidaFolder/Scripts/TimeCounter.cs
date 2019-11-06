using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public float timeRimit = 0;
    [SerializeField]
    Text text;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + timeRimit;

        if (timeRimit <= 0)
        {
            Debug.Log("timeRimit");
        }
        else
        {
            timeRimit -= Time.deltaTime;

        }
    }
}
