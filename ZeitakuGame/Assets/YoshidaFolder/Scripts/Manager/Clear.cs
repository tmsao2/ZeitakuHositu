using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    private void Start()
    {
        Fade.FadeIn();
    }
    public void FadeOutGoTitle()
    {
        Fade.FadeOut("SelectScene2");
    }

    public void FadeOutGoRetry()
    {
        Fade.FadeOut("GameScene");
    }
}
