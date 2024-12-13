using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Revelar : Hability
{
    override public void ActiveAbility()
    {
        base.ActiveAbility();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.activeRevelar = true;
        }
    }
    public override void DesActiveAbility()
    {
        base.DesActiveAbility();
        if (GameManager.Instance != null)
        {
            GameManager.Instance.activeRevelar = false;
        }
    }
}
