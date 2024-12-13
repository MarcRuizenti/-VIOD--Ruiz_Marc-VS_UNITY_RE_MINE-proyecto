using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityTreeAblity : MonoBehaviour
{
    public string nameAblity;
    public int coste;
    public string description;
    public bool IamActive = false;
    [SerializeField] private Extended extended;


    virtual public void ActiveAblity()
    {
        if (GameManager.Instance.pointsXp - coste >= 0)
        {
            GameManager.Instance.pointsXp -= coste;
        }
    }
    public void Extended()
    {
        extended.gameObject.SetActive(true);
        extended.Active(this);
    }
}
