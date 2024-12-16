using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{

    public float count = 0;
    public int EXP = 0;
    public float numNeed = 5;
    public int level = 0;
    [SerializeField] private Image slider;
    [SerializeField] private TMP_Text levelText;

    private void Update()
    {
        slider.fillAmount = EXP / numNeed;
        levelText.text = level.ToString();
    }

    public void AddEXP()
    {
        float num = count;
        for (int i = 0; i < num; i++)
        {
            if (EXP == numNeed)
            {
                numNeed += 5;
                EXP = 0;
                level++;
                GameManager.Instance.pointsXp++;
                continue;
            }
            EXP++;
        }

        count = 0;
    }
}
