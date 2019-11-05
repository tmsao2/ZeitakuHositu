using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    int gameTime = 0;
    static int selectedStage = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
        selectedStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime++;

        if (gameTime > 100)
        {
            GameManager.SceneChange("ResultScene");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.SceneChange("TitleScene");
        }
    }

    public static void Selected(int stage)
    {
        selectedStage = stage;
    }
}
