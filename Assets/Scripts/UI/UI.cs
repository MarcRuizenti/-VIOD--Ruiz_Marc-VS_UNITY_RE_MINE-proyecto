using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMenu()
    {
        GameManager.Instance.ResetGamePlay();
        GameManager.Instance.reset = true;
    }

    public void ChangeLevel()
    {
        GameManager.Instance.ChangeLevel();
    }
}
