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
        MoveUI();
    }

    void Title()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            eventFlag.enabled = true;
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
        beforeTrigger = Input.GetAxis("Vertical");
    }

    public void Back()
    {
        eventFlag.enabled = false;
        title.localPosition = selectPos;
        casol = 0;
    }

    void MoveUI()
    {
        if(eventFlag.enabled)
        {
            if (select.localPosition.x > -120)
            {
                title.localPosition = new Vector3(title.localPosition.x - speed, title.localPosition.y, title.localPosition.z);
                select.localPosition = new Vector3(select.localPosition.x - speed, select.localPosition.y, select.localPosition.z);
            }
            else
            {
                Select();
            }
        }
        else
        {
            if (title.localPosition.x > titlePos.x)
            {
                title.localPosition = new Vector3(title.localPosition.x - speed, title.localPosition.y, title.localPosition.z);
                select.localPosition = new Vector3(select.localPosition.x - speed, select.localPosition.y, select.localPosition.z);
            }
            else
            {
                Title();
                select.localPosition = selectPos;
            }
        }
    }
}
