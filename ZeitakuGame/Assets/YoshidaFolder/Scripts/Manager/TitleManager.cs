using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Submit") >0)
        {
            GameManager.SceneChange("SelectScene");
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            GameManager.SceneChange("GameScene");
        }
    }
}
