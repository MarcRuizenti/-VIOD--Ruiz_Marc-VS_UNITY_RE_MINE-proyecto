using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandClock : Hability
{
    override public void ActiveAbility()
    {
        base.ActiveAbility();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.activeSandClock = true;
        }
    }

    public override void DesActiveAbility()
    {
        base.DesActiveAbility();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.activeSandClock = false;
        }
    }
}
