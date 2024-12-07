using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : Hability
{

    override public void UseAbility()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.activeShild = true;

            energy--;
            isActive = false;
        }
    }
}
