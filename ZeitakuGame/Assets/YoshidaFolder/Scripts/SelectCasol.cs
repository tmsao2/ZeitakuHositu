using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCasol : MonoBehaviour
{
    [SerializeField]
    Button first;
    [SerializeField]
    Button second;
    [SerializeField]
    Button third;
    [SerializeField]
    Button back;

    float casol;
    private float beforeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        casol = 0;
        first = GameObject.Find("/Selects/first").GetComponent<Button>();
        second = GameObject.Find("/Selects/second").GetComponent<Button>();
        third = GameObject.Find("/Selects/third").GetComponent<Button>();
        back = GameObject.Find("/Selects/back").GetComponent<Button>();
        beforeTrigger = 0;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("" + casol);
        if (Input.GetAxis("Vertical") <0&& beforeTrigger==0)
        {
            casol++;
        }
        if (Input.GetAxis("Vertical") >0 && beforeTrigger==0)
        {
            casol--;
        }
        if (casol > 3)
        {
            casol = 0;
        }
        if (casol < 0)
        {
            casol=3;
        }
        switch (casol)
        {
            case (0): first.Select(); break;
            case (1): second.Select(); break;
            case (2): third.Select(); break;
            case (3): back.Select(); break;
        }
        beforeTrigger = Input.GetAxis("Vertical");
    }
}
