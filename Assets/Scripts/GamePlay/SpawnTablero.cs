using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnTablero : MonoBehaviour
{
    [Header("Distancias")]
    public float distanceCamera;
    public float alturaCamera;
    public float minDistance;
    public float maxDistance;
    private Vector3 centro;
    [Header("Objetos")]
    [SerializeField] private GameObject tablero;
    [SerializeField] private GameObject cube;
    [SerializeField] private Camera _camera;
    private LogicMap _logicMap;
    private int numCube = 11;

    private void Start()
    {
        SpwanTablero();
        minDistance = 1;
        maxDistance = 5;
    }
    public void SpwanTablero()
    {
        _logicMap = GetComponent<LogicMap>();
        if (GameManager.Instance != null)
        {
            numCube = GameManager.Instance.numCube;
        }

        for (int i = 0; i < numCube; i++)
        {
            for (int j = 0; j < numCube; j++)
            {
                for (int k = 0; k < numCube; k++)
                {
                    if (!_logicMap.esExterna(i, j, k)) continue;


                    GameObject temp = Instantiate(cube);

                    temp.gameObject.isStatic = true;

                    temp.transform.position = new Vector3(j, i, k);
                    temp.GetComponent<CubeLogic>().pos = new Vector3(j, i, k);
                    if (_logicMap != null)
                    {
                        temp.GetComponent<CubeLogic>()._logicMap = _logicMap;
                        _logicMap.AddCube(temp, i, j, k);
                    }
                }
            }
        }

        centro = Vector3.zero;
        int num = 0;
        foreach (GameObject cube in _logicMap.lista)
        {
            if (cube != null)
            {
                centro += cube.transform.position;
                num++;
            }
        }

        centro /= num;

        tablero.transform.position = centro;

        _camera.transform.position = centro + -_camera.transform.forward * numCube * distanceCamera + tablero.transform.up * numCube * alturaCamera;

        _camera.transform.parent = tablero.transform;

        _logicMap.SpawnMinas();

        _logicMap.SpawnNums();
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.numBanderas == GameManager.Instance.numMinas)
            {
                if (_logicMap.checkWin())
                {
                    GameManager.Instance.Win();
                }
            }

            if (GameManager.Instance.reset)
            {
                for (int y = 0; y < numCube; y++)
                {
                    for (int x = 0; x < numCube; x++)
                    {
                        for (int z = 0; z < numCube; z++) 
                        { 
                            if (_logicMap.lista[y, x, z])
                            {
                                Destroy(_logicMap.lista[y, x, z]);
                            }
                        }
                    }
                }
                GameManager.Instance.reset = false;
            }
        }
        else{
            if (_logicMap.checkWin())
            {
                SceneManager.LoadScene("Menu");
            }
        }
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        if (mouse != 0)
        {
            if (distanceCamera >= minDistance && distanceCamera <= maxDistance)
            {
                distanceCamera += mouse * 2;
                distanceCamera = Mathf.Clamp(distanceCamera, minDistance, maxDistance);
            }
        }

        _camera.transform.position = centro + -_camera.transform.forward * numCube * distanceCamera + tablero.transform.up * numCube * alturaCamera;

    }
}

