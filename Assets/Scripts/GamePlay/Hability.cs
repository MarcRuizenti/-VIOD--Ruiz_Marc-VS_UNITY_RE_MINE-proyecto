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

    private void Update()
    {
        if (isActive)
        {
            UseAbility();
        }
    }

    virtual public void UseAbility()
    {

    }

}
