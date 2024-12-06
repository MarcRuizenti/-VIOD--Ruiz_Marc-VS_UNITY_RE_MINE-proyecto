using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandClock : Hability
{
    private void Start()
    {
        name = "Sand Clock";
    }
    override public void UseAbility()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.activeSandClock = true;

            energy--;
            isActive = false;

        }
    }
}
