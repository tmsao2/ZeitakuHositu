using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    private void Start()
    {
        Fade.FadeIn();
        SoundManager.Instance.PlayBgm("clear");
    }
    public void Title()
    {
        Fade.FadeOut("SelectScene2");
        SoundManager.Instance.StopBgm();
    }

    public void Retry()
    {
        Fade.FadeOut("GameScene");
        SoundManager.Instance.StopBgm();
    }
}
