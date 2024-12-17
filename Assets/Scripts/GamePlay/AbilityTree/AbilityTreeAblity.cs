using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityTreeAblity : MonoBehaviour
{
    public string nameAblity;
    public int coste;
    public string description;
    public bool IAmActive = false;
    protected bool canActivate = false;
    [SerializeField] private Extended extended;

    [SerializeField] private Sprite frameBlock;
    [SerializeField] private Sprite iconBlock;
    [SerializeField] private Sprite iconDefault;
    [SerializeField] private Sprite frameDefault;
    [SerializeField] private GameObject frame;

    virtual protected void Update()
    {
        if (!transform.GetComponent<Button>().interactable)
        {
            transform.GetComponent<Button>().image.sprite = iconBlock;
            frame.GetComponent<Image>().sprite = frameBlock;
        }
        else
        {
            transform.GetComponent<Button>().image.sprite = iconDefault;
            frame.GetComponent<Image>().sprite = frameDefault;
        }
    }
    virtual public void ActiveAblity()
    {
        if (GameManager.Instance.pointsXp - coste >= 0)
        {
            GameManager.Instance.pointsXp -= coste;
            canActivate = true;

        }
    }
    public void Extended()
    {
        extended.gameObject.SetActive(true);
        extended.Active(this);
    }
}
