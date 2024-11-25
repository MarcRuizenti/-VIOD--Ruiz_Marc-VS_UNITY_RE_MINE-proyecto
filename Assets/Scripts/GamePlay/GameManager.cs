using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("GamePlay")]
    public int numCube;
    public int numMinas;
    public int numBanderas;
    [SerializeField] private SpawnTablero st;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddBandera()
    {
        numBanderas++;
    }

    public void RemoveBandera() 
    { 
        numBanderas--;
    }

    public void Win()
    {
        numBanderas = 0;
        SceneManager.LoadScene("Menu");
    }

    public void Loss()
    {
        numBanderas = 0;
        SceneManager.LoadScene("Menu");
    }
}
