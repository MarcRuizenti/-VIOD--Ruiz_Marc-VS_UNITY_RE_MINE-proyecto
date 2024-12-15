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

    public void ResetGamePlay()
    {
        GameManager.Instance.ResetGamePlay();
        GameManager.Instance.reset = true;
    }

    public void LoadMenu()
    {
        LoadScene("Menu");
    }
    public void ChangeLevel()
    {
        GameManager.Instance.ChangeLevel();
    }

    public void PlaySound(AudioClip audio)
    {
        SoundManager.Instance.EjecutarAudio(audio);
    }
}
