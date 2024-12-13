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
        bool have = false;
        int numAnility = 0;
        Hability temp = Instantiate(abilitys[a1]).GetComponent<Hability>();

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
            GameManager.Instance.ChangeLevel();
        }
        else
        {
            Destroy(temp);
            GameManager.Instance.habilityList[numAnility].LevelUp();
            GameManager.Instance.ChangeLevel();
        }

    }

    public void SelectAbility2()
    {
        bool have = false;
        int numAnility = 0;
        Hability temp = Instantiate(abilitys[a2]).GetComponent<Hability>();

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
            GameManager.Instance.ChangeLevel();
        }
        else
        {
            Destroy(temp.gameObject);
            GameManager.Instance.habilityList[numAnility].LevelUp();
            GameManager.Instance.ChangeLevel();
        }
    }
}
