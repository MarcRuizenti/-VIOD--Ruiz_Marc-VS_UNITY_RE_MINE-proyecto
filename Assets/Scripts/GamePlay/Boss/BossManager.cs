using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("Metoritos")]
    [SerializeField] private GameObject metirito;
    [SerializeField] private GameObject tablero;
    private bool activate = false;
    public bool canActivate = true;

    [Header("Timers")]
    public float timerStartSpawn;
    private float timerStartSpawnCounter;
    public float timeSpawn;
    private float timeSpawnCounter;
    public float numSpawn;

    [Header("UI")]
    [SerializeField] private GameObject lv;
    [SerializeField] private GameObject lvNum;
    [SerializeField] private GameObject BossText;

    [Header("Sounds")]
    [SerializeField] private AudioSource musicBase;
    [SerializeField] private AudioSource musicBoss;

    
    private void Update()
    {
        if (GameManager.Instance.levelnum == 10 && canActivate)
        {
            canActivate = false;
            activate = true;
            lv.SetActive(false);
            lvNum.SetActive(false);
            BossText.SetActive(true);
            musicBase.mute = true;
            musicBoss.mute = false;
            timerStartSpawnCounter = timerStartSpawn;
        }
        else if (GameManager.Instance.levelnum != 10)
        {
            activate = false;
            lv.SetActive(true);
            lvNum.SetActive(true);
            BossText.SetActive(false);
            musicBase.mute = false;
            musicBoss.mute = true;
        }

        if (activate)
        {
            if (timerStartSpawnCounter <= 0)
            {
                if (timeSpawnCounter <= 0)
                {
                    timeSpawnCounter = timeSpawn;

                    for (int i = 0; i < numSpawn; i++)
                    {
                        SpawnMetioritos();
                    }
                    numSpawn++;
                }
                else timeSpawnCounter -= Time.deltaTime;
            }
            else timerStartSpawnCounter -= Time.deltaTime;
        }
    }
    private void SpawnMetioritos()
    {
        int numCube = GameManager.Instance.numCube + 25;
        float x = 0, y = 0, z = 0;
        int cara = Random.Range(0, 6);

        switch (cara)
        {
            case 0:
                y = 0;
                x = Random.Range(GameManager.Instance.numCube + 15, numCube);
                z = Random.Range(GameManager.Instance.numCube + 15, numCube);
                break;

            case 1:
                y = numCube - 1;
                x = Random.Range(GameManager.Instance.numCube + 15, numCube);
                z = Random.Range(GameManager.Instance.numCube + 15, numCube);
                break;

            case 2:
                z = 0;
                x = Random.Range(GameManager.Instance.numCube + 15, numCube);
                y = Random.Range(GameManager.Instance.numCube + 15, numCube);
                break;

            case 3:
                z = numCube - 1;
                x = Random.Range(GameManager.Instance.numCube + 15, numCube);
                y = Random.Range(GameManager.Instance.numCube + 15, numCube);
                break;

            case 4:
                x = 0;
                y = Random.Range(GameManager.Instance.numCube + 15, numCube);
                z = Random.Range(GameManager.Instance.numCube + 15, numCube);
                break;

            case 5:
                x = numCube - 1;
                y = Random.Range(GameManager.Instance.numCube + 15, numCube);
                z = Random.Range(GameManager.Instance.numCube + 15, numCube);
                break;
        }

        GameObject temp = Instantiate(metirito, new Vector3(x, y, z), Quaternion.identity);

        temp.GetComponent<MovementMetiorito>().objetivo = tablero;

    }

    
}
