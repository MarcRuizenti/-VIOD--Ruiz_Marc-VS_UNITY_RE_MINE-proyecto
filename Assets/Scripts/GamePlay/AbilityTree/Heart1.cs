using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart1 : AbilityTreeAblity
{
    public int numHeart;

    override public void ActiveAblity()
    {
        if (!IamActive)
        {
            if (numHeart == 0)
            {
                base.ActiveAblity();
                GameManager.Instance.heartsActive[numHeart] = true;
                IamActive = true;
                return;
            }
            if (numHeart == 1)
            {
                if (GameManager.Instance.heartsActive[0])
                {
                    base.ActiveAblity();
                    GameManager.Instance.heartsActive[numHeart] = true;
                    IamActive = true;
                    return;
                }
            }
            if (numHeart == 2)
            {
                if (GameManager.Instance.heartsActive[1])
                {
                    base.ActiveAblity();
                    GameManager.Instance.heartsActive[numHeart] = true;
                    IamActive = true;
                    return;
                }
            }
        }
    }
}
