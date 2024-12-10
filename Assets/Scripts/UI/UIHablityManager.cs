using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIHablityManager : MonoBehaviour
{

    [SerializeField] private GameObject[] panels;

    [SerializeField] private TMP_Text[] nameH;
    [SerializeField] private TMP_Text[] level;
    [SerializeField] private TMP_Text[] energy;
    [SerializeField] private TMP_Text[] maxEnergy;

    public Color defaultColor;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            int num = GameManager.Instance.habilityList.Count;
            for (int i = 0; i < 3; i++) 
            {
                if (i < num)
                {
                    panels[i].SetActive(true);
                    nameH[i].text = GameManager.Instance.habilityList[i].name;
                    level[i].text = GameManager.Instance.habilityList[i].level.ToString();
                    energy[i].text = GameManager.Instance.habilityList[i].energy.ToString();
                    maxEnergy[i].text = GameManager.Instance.habilityList[i].maxEnergy.ToString();
                    if (GameManager.Instance.habilityList[i].isActive)
                    {
                        panels[i].GetComponent<Image>().color = GameManager.Instance.habilityList[i].color;
                    }
                    else
                    {
                        panels[i].GetComponent<Image>().color = defaultColor;
                    }
                }
                else
                {
                    panels[i].SetActive(false);
                }
            }
        }
    }
}
