using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hability : MonoBehaviour
{
    [Header("Logic")]
    public UIHablityManager ablityManagerUI = null;
    public bool isActive = false;
    public int energy = 0;
    public int maxEnergy = 0;
    public int level = 1;
    public string nameHability;
    public string description;
    public int numCopis = 0;
    public int[] numCopisLevelUp;
    public Color color;

    [Header("Sounds")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _recarge;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    virtual public void ActiveAbility()
    {
        isActive = true;
    }

    virtual public void DesActiveAbility()
    {
        isActive = false;
    }

    virtual public void LevelUp()
    {
        if (numCopisLevelUp.Length > level)
        {
            numCopis++;
            if (numCopisLevelUp[level - 1] == numCopis)
            {
                level++;
                numCopis = 0;
                energy = maxEnergy;
            }
        }
    }

    public void AddEnergy()
    {
        if (energy + 1 <= maxEnergy)
        {
            _audioSource.PlayOneShot(_recarge);
            energy++;
            GameManager.Instance.timerCounter -= 60;
            if (ablityManagerUI != null)
            {
                ablityManagerUI.ActualizeAblityUI();
            }
        }
    }
}
