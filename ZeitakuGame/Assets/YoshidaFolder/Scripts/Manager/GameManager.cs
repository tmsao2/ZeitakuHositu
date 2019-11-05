using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class GameManager
{
    public static void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
