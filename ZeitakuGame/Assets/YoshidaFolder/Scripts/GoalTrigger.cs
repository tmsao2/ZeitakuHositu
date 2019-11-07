using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{

    [SerializeField]
    GameObject playerShip;

    [SerializeField]
    GameObject finishUI;

    int time=0;
    bool end=false;

    // Start is called before the first frame update
    void Start()
    {
        if(playerShip==null)
        {
            playerShip = GameObject.Find("boat");
        }
        //if(finishUI==null)
        {
            //finishUI = Find("GoalCan");
        }
        time = 0;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(end == false)
        { return; }
        if (time>200)
        {
            Fade.FadeOut("ClearScene");
            //GameManager.SceneChange("ResultScene");
        }
        else
        {
            time++;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(playerShip.tag==other.tag)
        {
            Debug.Log("end");
            end = true;
            finishUI.SetActive(true);
        }
    }
}
