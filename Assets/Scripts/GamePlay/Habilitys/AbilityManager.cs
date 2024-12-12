using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] abilitys;

    [Header("Paneles")]
    [SerializeField] private TMP_Text[] names;
    [SerializeField] private TMP_Text[] energys;
    [SerializeField] private TMP_Text[] description;

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

    public void SelectAbility1()
    {
        if (GameManager.Instance.habilityList.Count < 3)
        {
            Hability temp = Instantiate(abilitys[a1]).GetComponent<Hability>();
            GameManager.Instance.habilityList.Add(temp);
            GameManager.Instance.ChangeLevel();
        }
    }

    public void SelectAbility2()
    {
        if (GameManager.Instance.habilityList.Count < 3)
        {
            Hability temp = Instantiate(abilitys[a2]).GetComponent<Hability>();
            GameManager.Instance.habilityList.Add(temp);
            GameManager.Instance.ChangeLevel();
        }
    }
}
