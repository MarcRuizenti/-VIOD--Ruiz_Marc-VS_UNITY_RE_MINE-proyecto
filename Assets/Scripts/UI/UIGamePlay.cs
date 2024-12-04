using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGamePlay : MonoBehaviour
{

    [Header("UI")]
    public TMP_Text numMinasText;
    public TMP_Text numBanderasText;

    private void Update()
    {
        if (numMinasText != null) numMinasText.text = GameManager.Instance.numMinas.ToString();

        if (numBanderasText != null) numBanderasText.text = GameManager.Instance.numBanderas.ToString();
    }
}
