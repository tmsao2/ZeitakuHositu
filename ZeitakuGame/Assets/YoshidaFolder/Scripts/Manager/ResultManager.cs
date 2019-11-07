using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private void Start()
    {
        Fade.FadeIn();
    }
    public void Title()
    {
        Fade.FadeOut("SelectScene2");
    }

    public void Retry()
    {
        Fade.FadeOut("GameScene");
    }
}
