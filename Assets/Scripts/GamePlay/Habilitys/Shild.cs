using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : Hability
{
    override public void ActiveAbility()
    {
        base.ActiveAbility();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.activeShild = true;
        }
    }

    public override void DesActiveAbility()
    {
        base.DesActiveAbility();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.activeShild = false;
        }
    }
}
