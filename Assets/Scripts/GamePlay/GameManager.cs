
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;



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
    public float timer;
    public float timerCounter;
    public int num0;

    [Header("Habilitys")]
    public List<Hability> habilityList;
    public bool oneHanilityActive = false;
    public bool activeSandClock = false;
    public bool activeShild = false;
    public bool activeRevelar = false;
    public Hability habilityActive = null;

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

        if (Input.GetButtonDown("Fire1") && canMove && habilityList.Count != 0 )
        {
            if (!oneHanilityActive)
            {
                if (habilityList[0].energy != 0)
                {
                    habilityList[0].ActiveAbility();
                    oneHanilityActive = true;
                    habilityActive = habilityList[0];
                }
            }
            else
            {
                if (!habilityList[0].isActive)
                {
                    if (habilityList[0].energy != 0)
                    {
                        habilityActive.DesActiveAbility();
                        habilityList[0].ActiveAbility();
                        habilityActive = habilityList[0];
                    }
                }
                else
                {
                    habilityActive.DesActiveAbility();
                    habilityActive = null;
                    oneHanilityActive = false;
                }
            }
        }
        if (Input.GetButtonDown("Fire2") && canMove && habilityList.Count > 0)
        {
            if (!oneHanilityActive)
            {
                if (habilityList[1].energy != 0)
                {
                    habilityList[1].ActiveAbility();
                    oneHanilityActive = true;
                    habilityActive = habilityList[1];
                }
            }
            else
            {
                if (!habilityList[1].isActive)
                {
                    if (habilityList[1].energy != 0)
                    {
                        habilityActive.DesActiveAbility();
                        habilityList[1].ActiveAbility();
                        habilityActive = habilityList[1];
                    }
                }
                else
                {
                    habilityActive.DesActiveAbility();
                    habilityActive = null;
                    oneHanilityActive = false;
                }
            }
        }
        if (Input.GetButtonDown("Fire3") && canMove && habilityList.Count > 1)
        {
            if (!oneHanilityActive)
            {
                if (habilityList[2].energy != 0)
                {
                    habilityList[2].ActiveAbility();
                    oneHanilityActive = true;
                    habilityActive = habilityList[2];
                }
            }
            else
            {
                if (!habilityList[2].isActive)
                {
                    if (habilityList[2].energy != 0)
                    {
                        habilityActive.DesActiveAbility();
                        habilityList[2].ActiveAbility();
                        habilityActive = habilityList[2];
                    }
                }
                else
                {
                    habilityActive.DesActiveAbility();
                    habilityActive = null;
                    oneHanilityActive = false;
                }
            }
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
        numBanderas = 0;
        levelnum++;
        if (levelnum % 10 == 0)
        {
            numCube++;
            timer += 60;
        }
        numMinas += 1;
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
