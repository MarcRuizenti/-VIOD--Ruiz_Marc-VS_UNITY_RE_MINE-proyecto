using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart1 : AbilityTreeAblity
{
    public int numHeart;

    override protected void Update()
    {
        if (numHeart != 0)
        {
            if (!GameManager.Instance.heartsActive[numHeart - 1])
            {
                transform.GetComponent<Button>().interactable = false;
            }
            else
            {
                transform.GetComponent<Button>().interactable = true;
            }
        }

        base.Update();
    }

    override public void ActiveAblity()
    {
        if (!IAmActive)
        {
            if (numHeart == 0)
            {
                base.ActiveAblity();
                GameManager.Instance.heartsActive[numHeart] = true;
                IAmActive = true;
                return;
            }
            if (numHeart == 1)
            {
                if (GameManager.Instance.heartsActive[0])
                {
                    base.ActiveAblity();
                    GameManager.Instance.heartsActive[numHeart] = true;
                    IAmActive = true;
                    return;
                }
            }
            if (numHeart == 2)
            {
                if (GameManager.Instance.heartsActive[1])
                {
                    base.ActiveAblity();
                    GameManager.Instance.heartsActive[numHeart] = true;
                    IAmActive = true;
                    return;
                }
            }
        }
    }
}
