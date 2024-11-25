using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class SpawnTablero : MonoBehaviour
{
    [Header("Distancias")]
    public float distanceCamera;
    public float alturaCamera;
    [Header("Objetos")]
    [SerializeField] private GameObject tablero;
    [SerializeField] private GameObject cube;
    [SerializeField] private Camera _camera;
    private LogicMap _logicMap;


    private void Start()
    {
        SpwanTablero();
    }
    public void SpwanTablero()
    {
        _logicMap = GetComponent<LogicMap>();

        int numCube = GameManager.Instance.numCube;

        for (int i = 0; i < numCube; i++)
        {
            for (int j = 0; j < numCube; j++)
            {
                for (int k = 0; k < numCube; k++)
                {
                    if (!_logicMap.esExterna(i, j, k)) continue;


                    GameObject temp = Instantiate(cube);

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

        Vector3 centro = Vector3.zero;
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
        if (GameManager.Instance.numBanderas == GameManager.Instance.numMinas)
        {
            if (_logicMap.checkWin())
            {
                GameManager.Instance.Win();
            }
        }
    }
}

