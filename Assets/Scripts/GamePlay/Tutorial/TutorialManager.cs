using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    [Header("Timer")]
    private float timerCount;
    private float timer = 5;
    private bool timerCanCount = true;
    private bool firts = true;

    [Header("UI")]
    [SerializeField] private Canvas dialogoPanel;
    [SerializeField] private TMP_Text dialogoText;
    [SerializeField] private GameObject panelTuto;

    [Header("Dialogos")]
    [SerializeField, TextArea(4, 6)] private string[] dialogos;
    private bool dialogoStart = false;
    private int lineIndex = 0;
    [SerializeField] private float typingTime = 0.05f;
    private bool canPressE = false;

    [Header("Sound")]
    [SerializeField] private List<AudioClip> SoudsKeysList;

    [Header("UIGamePLay")]
    [SerializeField] private Canvas UIGamePlay;
    [SerializeField] private GameObject panelLevel;
    [SerializeField] private GameObject panelTimer;
    [SerializeField] private GameObject panelnumMinas;
    private void Start()
    {
        if (timerCanCount)
        {
            UIGamePlay.gameObject.SetActive(false);
            timerCount = timer;
        }
        NextDialog();
    }

    void Update()
    {
        if (!GameManager.Instance.canMove && !GameManager.Instance.canClickQ && !canPressE)
        {

            if (Input.GetButtonDown("Q"))
            {
                lineIndex = 5;
                NextDialog();
                GameManager.Instance.canClickQ = true;
                canPressE = true;
                dialogoStart = true;
            }

            if (Input.GetButtonDown("E"))
            {
                NextDialog();
            }
        }

        if (GameManager.Instance.firtClickCube && firts)
        {
            timerCanCount = false;
            canPressE = false;
            dialogoStart = true;
            lineIndex = 1;
            StartDialogue();
            firts = false;
            GameManager.Instance.canMove = false;
        }

        if (timerCount > 0 && timerCanCount)
        {
            timerCount -= Time.deltaTime;
        }
        else if (timerCanCount)
        {
            StartDialogue();
            timerCanCount = false;
        }

        if (Input.GetButtonDown("E") && canPressE && dialogoStart)
        {
            NextDialog();
        }
    }

    private void NextDialog()
    {        
        if (dialogoText.text == dialogos[lineIndex])
        {
            NextDialogueLine();
        }
        else
        {
            StopAllCoroutines();
            dialogoText.text = dialogos[lineIndex];
        }
    }
    private void StartDialogue()
    {
        dialogoPanel.gameObject.SetActive(true);

        StartCoroutine(ShowLine());
    }
    private void NextDialogueLine()
    {
        lineIndex++;
        switch (lineIndex)
        {
            case 5:
                NextDialogueLine();
                return;
            case 6:
                panelLevel.transform.parent = panelTuto.transform;
                break;
            case 7:
                panelLevel.transform.parent = UIGamePlay.transform;
                panelTimer.transform.parent = panelTuto.transform;
                break;
            case 8:
                panelTimer.transform.parent = UIGamePlay.transform;
                panelnumMinas.transform.parent = panelTuto.transform;
                break;
            case 9:
                panelnumMinas.transform.parent = UIGamePlay.transform;
                UIGamePlay.gameObject.SetActive(true);
                dialogoPanel.gameObject.SetActive(false);
                GameManager.Instance.canMove = true;
                return;
            default:
                break;
        }
        if (lineIndex < dialogos.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            dialogoStart = false;
            dialogoPanel.gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowLine()
    {
        dialogoText.text = string.Empty;

        foreach (char c in dialogos[lineIndex])
        {
            int rand = Random.Range(0, SoudsKeysList.Count);
            dialogoText.text += c;
            SoundManager.Instance.EjecutarAudio(SoudsKeysList[rand]);
            yield return new WaitForSeconds(typingTime);
        }
    }
}
