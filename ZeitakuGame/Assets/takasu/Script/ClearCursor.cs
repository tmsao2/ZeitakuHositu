using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ClearCursor : MonoBehaviour
{
    [SerializeField]
    Button[] buttons;
    [SerializeField]
    EventSystem eventFlag;

    float casol;
    private float beforeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        casol = 0;
        beforeTrigger = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 && beforeTrigger == 0)
        {
            casol++;
        }
        if (Input.GetAxis("Horizontal") < 0 && beforeTrigger == 0)
        {
            casol--;
        }
        if (casol > buttons.Length - 1)
        {
            casol = buttons.Length - 1;
        }
        if (casol < 0)
        {
            casol = 0;
        }
        for (int i = 0; i < buttons.Length; ++i)
        {
            if (casol == i)
            {
                buttons[i].Select();
            }
        }
        if(Fade.isFadeOut)
        {
            eventFlag.enabled = false;
        }

        beforeTrigger = Input.GetAxis("Horizontal");
    }
}
