using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UIHablityManager : MonoBehaviour
{

    [SerializeField] private GameObject[] panels;

    [SerializeField] private TMP_Text[] nameH;
    [SerializeField] private TMP_Text[] level;
    [SerializeField] private TMP_Text[] energy;
    [SerializeField] private TMP_Text[] maxEnergy;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            int num = GameManager.Instance.habilityList.Count;
            for (int i = 0; i < num; i++) 
            { 
                panels[i].SetActive(true);
                nameH[i].text = GameManager.Instance.habilityList[i].name;
                level[i].text = GameManager.Instance.habilityList[i].level.ToString();
                energy[i].text = GameManager.Instance.habilityList[i].energy.ToString();
                maxEnergy[i].text = GameManager.Instance.habilityList[i].maxEnergy.ToString();

            }
        }
    }
}
