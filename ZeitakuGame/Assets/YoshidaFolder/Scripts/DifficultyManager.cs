using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [System.Serializable]
    public enum difficulty
    {
        easy=0,
        normal=1,
        hard=2
    }

    [SerializeField]
    GameObject easyStage;
    [SerializeField]
    GameObject normalStage;
    [SerializeField]
    GameObject difficultStage;

    [SerializeField]
    TimeCounter timeCounter;

    public void SetDifficulty(int difint)
    {
        timeCounter = GameObject.Find("TimeManager").GetComponent<TimeCounter>();
        difficulty diff=(difficulty)difint;
        GameObject stageCreater = null;
        int timeRimit = 0;
        switch (diff)
        {
            case (difficulty.easy): stageCreater = easyStage; timeRimit = 120; ; break;
            case (difficulty.normal): stageCreater = normalStage; timeRimit = 180; break;
            case (difficulty.hard): stageCreater = difficultStage; timeRimit = 240; break;
        }
        timeCounter.SetLimit(timeRimit);
        TimeCounter.timeRimitMax = timeRimit;
        //stageCreater=Instantiate(stageCreater, new Vector3(0, 0, 0), new Quaternion());
        stageCreater.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        SetDifficulty(StageMemoy.stage);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
