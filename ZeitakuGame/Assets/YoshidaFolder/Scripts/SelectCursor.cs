using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCursor : MonoBehaviour
{
    [SerializeField]
    EventSystem eventFlag;

    [SerializeField]
    Button[] buttons;
    
    [SerializeField]
    RectTransform title;
    [SerializeField]
    RectTransform select;

    [SerializeField]
    float speed;

    float casol;
    private float beforeTrigger;
    bool slide = false;

    Vector3 titlePos;
    Vector3 selectPos;

    // Start is called before the first frame update
    void Start()
    {
        casol = 0;
        beforeTrigger = 0;
        titlePos = title.localPosition;
        selectPos = select.localPosition;
        eventFlag.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(eventFlag.enabled)
        {
            Select();
        }
        else
        {
            Title();
        }
    }

    void Title()
    {
        if(Input.GetButtonDown("Submit"))
        {
            slide = true;
            
        }
        if(slide)
        {
            if (select.localPosition.x > -120)
            {
                title.localPosition = new Vector3(title.localPosition.x - speed, title.localPosition.y, title.localPosition.z);
                select.localPosition = new Vector3(select.localPosition.x - speed, select.localPosition.y, select.localPosition.z);
            }
            else
            {
                slide = false;
                eventFlag.enabled = true;
            }
        }
    }

    void Select()
    {
        Debug.Log("" + casol);
        if (Input.GetAxis("Vertical") < 0 && beforeTrigger == 0)
        {
            casol++;
        }
        if (Input.GetAxis("Vertical") > 0 && beforeTrigger == 0)
        {
            casol--;
        }
        if (casol > buttons.Length-1)
        {
            casol = 0;
        }
        if (casol < 0)
        {
            casol = buttons.Length - 1;
        }
        for(int i=0;i<buttons.Length;++i)
        {
            if(casol==i)
            {
                buttons[i].Select();
            }
        }
        if (Fade.isFadeOut)
        {
            eventFlag.enabled = false;
        }
        beforeTrigger = Input.GetAxis("Vertical");

    }
}
