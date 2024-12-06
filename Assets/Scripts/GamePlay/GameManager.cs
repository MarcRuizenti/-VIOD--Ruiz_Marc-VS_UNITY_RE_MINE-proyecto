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
    public bool activeSandClock = false;
    public bool canMove = true;
    public bool reset = false;
    public float timer;
    public float timerCounter;
    public int num0;

    public List<Hability> habilityList;

    [Header("UI")]
    public TMP_Text winText;
    public TMP_Text lossText;
    public TMP_Text timerText;
    public TMP_Text numMinasText;
    public TMP_Text numBanderasText;
    public TMP_Text levelText;

    public Button nextLevel;
    public Button menuLoss;
    public Button menuWin;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        timerCounter = timer;
    }
    private void Update()
    {

        if (Input.GetButtonDown("Fire1") && canMove && habilityList.Count != 0)
        {
            if (habilityList[0].energy != 0) habilityList[0].isActive = true;
        }

        if (timerCounter > 0 && canMove)
        {
            timerCounter -= Time.deltaTime;
        }
        else if (timerCounter < 0)
        {
            timerCounter = 0;
            Loss();
        }

        int min = Mathf.FloorToInt(timerCounter / 60);
        int sec = Mathf.FloorToInt(timerCounter % 60);


        if (timerText != null) timerText.text = string.Format("{0:00}:{1:00}", min, sec);  

        if (numMinasText != null) numMinasText.text = numMinas.ToString();

        if (numBanderasText != null) numBanderasText.text = numBanderas.ToString();

        if (levelText != null) levelText.text = levelnum.ToString();
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
            numCube++;
            numMinas += levelnum;
            timer += 60;
        }
        timerCounter = timer;
        canMove = true;
        reset = true;
        ChangeUIWin(false);

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
    }
}
