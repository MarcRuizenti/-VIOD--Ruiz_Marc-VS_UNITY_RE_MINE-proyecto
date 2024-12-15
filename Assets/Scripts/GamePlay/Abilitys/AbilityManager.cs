using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private UIHablityManager hablityManagerUI;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] abilitys;

    [Header("Paneles")]
    [SerializeField] private TMP_Text[] names;
    [SerializeField] private TMP_Text[] energys;
    [SerializeField] private TMP_Text[] description;

    [Header("Sounds")]
    [SerializeField] private AudioClip _addAbility;

    private int a1 = 0;
    private int a2 = 0;

    public void ActiveSelectionAbilitys()
    {
        a1 = Random.Range(0, abilitys.Length);
        a2 = Random.Range(0, abilitys.Length);

        if (a1 == a2)
        {
            bool ok = false;
            while (!ok)
            {
                a2 = Random.Range(0, abilitys.Length);
                if (a1 != a2)
                {
                    ok = true;
                }
            }
        }

        for (int i = 0; i < names.Length; i++)
        {
            if (i == 0)
            {
                names[i].text = abilitys[a1].GetComponent<Hability>().nameHability;
                energys[i].text = abilitys[a1].GetComponent<Hability>().energy.ToString();
                description[i].text = abilitys[a1].GetComponent<Hability>().description;

            }
            if (i == 1)
            {
                names[i].text = abilitys[a2].GetComponent<Hability>().nameHability;
                energys[i].text = abilitys[a2].GetComponent<Hability>().energy.ToString();
                description[i].text = abilitys[a2].GetComponent<Hability>().description;
            }
        }
    }

    public void SelectAbility(int num)
    {
        bool have = false;
        int numAnility = 0;
        Hability temp = null;

        if (num == 1)
        {
            temp = Instantiate(abilitys[a1]).GetComponent<Hability>();
        }
        else
        {
            temp = Instantiate(abilitys[a2]).GetComponent<Hability>();
        }
        for (int i = 0; i < GameManager.Instance.habilityList.Count; i++)
        {
            if (GameManager.Instance.habilityList[i].nameHability == temp.nameHability)
            {
                have = true;
                numAnility = i;
                break;
            }
        }

        if (!have)
        {
            GameManager.Instance.habilityList.Add(temp);
            SoundManager.Instance.EjecutarAudio(_addAbility);
            GameManager.Instance.ChangeLevel();
            hablityManagerUI.ActualizeAblityUI();
            temp.ablityManagerUI = hablityManagerUI;
        }
        else
        {
            Destroy(temp);
            GameManager.Instance.habilityList[numAnility].LevelUp();
            GameManager.Instance.ChangeLevel();
            hablityManagerUI.ActualizeAblityUI();
        }

    }
}
