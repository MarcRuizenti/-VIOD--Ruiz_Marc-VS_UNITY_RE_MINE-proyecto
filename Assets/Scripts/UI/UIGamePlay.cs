using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGamePlay : MonoBehaviour
{

    [Header("UI")]
    public TMP_Text numMinasText;
    public TMP_Text numBanderasText;
    // Start is called before the first frame update
    private void Start()
    {
        if (numMinasText != null) numMinasText.text = GameManager.Instance.numMinas.ToString();
    }

    private void Update()
    {
        if (numBanderasText != null) numBanderasText.text = GameManager.Instance.numBanderas.ToString();
    }
}
