using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [Header("Metoritos")]
    [SerializeField] private GameObject metirito;

    private void Start()
    {
        SpawnMetioritos();
    }
    private void SpawnMetioritos()
    {
        int numCube = GameManager.Instance.numCube + 15;
        float x = 0, y = 0, z = 0;
        int cara = Random.Range(0, 6);

        switch (cara)
        {
            case 0:
                y = 0;
                x = Random.Range(0, numCube);
                z = Random.Range(0, numCube);
                break;

            case 1:
                y = numCube - 1;
                x = Random.Range(0, numCube);
                z = Random.Range(0, numCube);
                break;

            case 2:
                z = 0;
                x = Random.Range(0, numCube);
                y = Random.Range(0, numCube);
                break;

            case 3:
                z = numCube - 1;
                x = Random.Range(0, numCube);
                y = Random.Range(0, numCube);
                break;

            case 4:
                x = 0;
                y = Random.Range(0, numCube);
                z = Random.Range(0, numCube);
                break;

            case 5:
                x = numCube - 1;
                y = Random.Range(0, numCube);
                z = Random.Range(0, numCube);
                break;
        }

        Instantiate(metirito, new Vector3(x, y, z), Quaternion.identity);
    } 
}
