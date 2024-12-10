using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hability : MonoBehaviour
{
    public bool isActive = false;
    public int energy = 0;
    public int maxEnergy = 0;
    public int level = 1;
    public string nameHability;

    public Color color;

    virtual public void ActiveAbility()
    {
        isActive = true;
    }

    virtual public void DesActiveAbility()
    {
        isActive = false;
    }

}
