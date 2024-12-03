using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameManager : MonoBehaviour
{
    [Header("GamePlay")]
    public int numCube;
    public int numMinas;
    public int numCubeBase;
    public int numMinasBase;
    public int numBanderas;
    public int levelnum = 1;
    private bool Iwin;
    public bool canMove = true;
    public bool reset = false;
    [Header("UI")]
    public TMP_Text winText;
    public TMP_Text lossText;

    public Button nextLevel;
    public Button menuLoss;
    public Button menuWin;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddBandera()
    {
        numBanderas++;
    }

    public void RemoveBandera() 
    { 
        numBanderas--;
    }

    public void Win()
    {
        canMove = false;
        numBanderas = 0;
        ChangeUIWin(true);
        Iwin = true;
    }

    public void Loss()
    {
        canMove = false;
        numBanderas = 0;
        ChangeUILoss(true);
        Iwin = false;
    }

    public void ChangeUILoss(bool newBool)
    {
        if (lossText != null && menuLoss)
        {
            lossText.gameObject.SetActive(newBool);
            menuLoss.gameObject.SetActive(newBool);
        }
    }

    public void ChangeUIWin(bool newBool)
    {
        if (winText != null && nextLevel != null && menuWin != null)
        {
            winText.gameObject.SetActive(newBool);
            nextLevel.gameObject.SetActive(newBool);
            menuWin.gameObject.SetActive(newBool);
        }
    }

    public void ChangeLevel()
    {
        levelnum++;
        if (levelnum % 10 == 0)
        {
            ChangeUIWin(false);

            numCube++;
            numMinas += 10;
        }
        canMove = true;
    }

    public void ResetGamePlay()
    {
        levelnum = 1;
        numCube = numCubeBase;
        numMinas = numMinasBase;
        if (Iwin)
        {
            ChangeUIWin(false);
        }
        else
        {
            ChangeUILoss(false);
        }
        canMove = true;
        reset = true;
    }
}
