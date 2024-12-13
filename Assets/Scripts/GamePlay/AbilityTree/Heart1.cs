using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart1 : AbilityTreeAblity
{
    public int numHeart;

    override public void ActiveAblity()
    {
        base.ActiveAblity();
        GameManager.Instance.heartsActive[numHeart] = true;
    }
}
