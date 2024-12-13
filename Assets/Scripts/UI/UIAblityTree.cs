using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIAblityTree : MonoBehaviour
{
    [SerializeField] private TMP_Text points;
    
    void Update()
    {
        points.text = GameManager.Instance.pointsXp.ToString();
    }
}
