
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
    public bool Iwin = false;
    public bool canMove = true;
    public bool reset = false;
    public float timer;
    public float timerCounter;
    public int num0;
    public bool selectionAbility = false;
    public int pointsXp;

    [Header("Abilitys")]
    public List<Hability> habilityList;
    public bool oneHanilityActive = false;
    public bool activeSandClock = false;
    public bool activeShild = false;
    public bool activeRevelar = false;
    public Hability habilityActive = null;
    [SerializeField] private GameObject abilityManager;

    [Header("AbilityTree")]

    [SerializeField] private Canvas abilityTree;

    [Header("UI")]
    [SerializeField] private Canvas canvasGamePlay;
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

    [Header("HeartSystem")]
    public bool[] heartLive;

    public bool[] heartsActive;

    public Image[] hearts;

    public Sprite breakHeart;
    public Sprite heart;


    private void Awake()
    {
        Instance = this;
        heartsActive = new bool[3];
        heartLive = new bool[3];
        for (int i = 0; i < heartLive.Length; i++)
        {
            heartLive[i] = true;
        }
    }
    private void Start()
    {
        timerCounter = timer;
    }
    private void Update()
    {

        if (heartsActive[0])
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (heartsActive[i])
                {
                    hearts[i].gameObject.SetActive(true);
                    if (heartLive[i])
                    {
                        hearts[i].sprite = heart;
                    }
                    else
                    {
                        hearts[i].sprite = breakHeart;
                    }
                }
            }
        }

        if (Input.GetButtonDown("Fire1") && canMove && habilityList.Count != 0 )
        {
            AbilityLogicActivate(0);
        }
        if (Input.GetButtonDown("Fire2") && canMove && habilityList.Count > 1)
        {
            AbilityLogicActivate(1);
        }
        if (Input.GetButtonDown("Fire3") && canMove && habilityList.Count > 2)
        {
            AbilityLogicActivate(2);
        }

        if (Input.GetButtonDown("Q"))
        {
            if (canMove)
            {
                canMove = false;
                abilityTree.gameObject.SetActive(true);
                canvasGamePlay.gameObject.SetActive(false);
            }
            else
            {
                canMove = true;
                abilityTree.gameObject.SetActive(false);
                canvasGamePlay.gameObject.SetActive(true);
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

    public void AbilityLogicActivate(int num)
    {
        if (!oneHanilityActive)
        {
            if (habilityList[num].energy != 0)
            {
                habilityList[num].ActiveAbility();
                oneHanilityActive = true;
                habilityActive = habilityList[num];
            }
        }
        else
        {
            if (!habilityList[num].isActive)
            {
                if (habilityList[num].energy != 0)
                {
                    habilityActive.DesActiveAbility();
                    habilityList[num].ActiveAbility();
                    habilityActive = habilityList[num];
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
        Iwin = true;
        int temp = levelnum + 1;
        if (temp % 2 == 0 && !selectionAbility)
        {
            numCube++;
            timerCounter += 60;
            ActiveSelectionAbilitisCanvas();
            return;
        }
        ChangeUIWin(true);
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
        numBanderas = 0;
        numMinas += 3;
        timerCounter = timer/2 + timerCounter;
        canMove = true;
        reset = true;
        if (selectionAbility)
        {
            canvasGamePlay.gameObject.SetActive(true);
            abilityManager.SetActive(false);
            selectionAbility = false;
        }
        else
        {
            ChangeUIWin(false);
        }

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

    public void DesactiveAbility()
    {
        oneHanilityActive = false;
        habilityActive.energy--;
        habilityActive.DesActiveAbility();
        habilityActive.ablityManagerUI.ActualizeAblityUI();
        habilityActive = null;
    }

    public void ActiveSelectionAbilitisCanvas()
    {
        selectionAbility = true;
        canvasGamePlay.gameObject.SetActive(false);
        abilityManager.SetActive(true);
        abilityManager.GetComponent<AbilityManager>().ActiveSelectionAbilitys();
    }

    public void RegenHeart()
    {
        for (int i = 0; i < heartLive.Length; i++)
        {
            if (!heartLive[i])
            {
                heartLive[i] = true;
            }
        }
    }
}