using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    int waitTime = 0;
    bool timerStart;

    // Start is called before the first frame update
    void Start()
    {
        Fade.FadeIn();
        waitTime = 0;
        timerStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!timerStart)
        {
            return;
        }


        if(++waitTime<50)
        {
            waitTime++;
        }
        else
        { 
            SoundManager.Instance.StopBgm();
        }
        //if (Input.GetAxis("Submit") > 0)
        //{
        //    GameManager.SceneChange("GameScene");
        //}
    }

   public void BegginerStage()
    {
        if (!timerStart)
        {
            SoundManager.Instance.PlaySe("ButtonDecision");
        }
        timerStart = true;
        Fade.FadeOut("GameScene");
    }
}
