using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart1 : AbilityTreeAblity
{

    override public void ActiveAblity()
    {
        base.ActiveAblity();
        GameManager.Instance.heartsActive[0] = true;
    }
}
