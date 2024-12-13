using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Extended : MonoBehaviour
{
    [SerializeField] private TMP_Text nameAblityText;
    [SerializeField] private TMP_Text costeText;
    [SerializeField] private TMP_Text descriptionText;

    private AbilityTreeAblity abilitySelected;
    public void Active(AbilityTreeAblity ability) 
    {
        nameAblityText.text = ability.nameAblity;
        costeText.text = ability.coste.ToString();
        descriptionText.text = ability.description;

        abilitySelected = ability;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Desactive();
        }
    }

    public void ActiveAblity()
    {
        abilitySelected.ActiveAblity();
        Desactive();
    }

    public void Desactive()
    {
        transform.gameObject.SetActive(false);
    }
}
