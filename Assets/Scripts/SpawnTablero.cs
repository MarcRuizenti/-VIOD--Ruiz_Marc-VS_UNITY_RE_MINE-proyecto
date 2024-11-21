using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class SpawnTablero : MonoBehaviour
{
    [SerializeField] private GameObject tablero;
    [SerializeField] private GameObject cube;
    [SerializeField] private Camera _camera;
    private LogicMap _logicMap;

    void Start()
    {
        _logicMap = GetComponent<LogicMap>();

        int numCube = GameManager.Instance.numCube;

        for (int i = 0; i < numCube; i++)
        {
            for (int j = 0; j < numCube; j++)
            {
                for (int k = 0; k < numCube; k++)
                {
                    bool esExterno = i == 0 || i == numCube - 1 ||
                                     j == 0 || j == numCube - 1 ||
                                     k == 0 || k == numCube - 1;

                    if (!esExterno) continue;

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

        _camera.transform.position = centro + -_camera.transform.forward * numCube * 2 + tablero.transform.up * numCube * 0.04f;

        _camera.transform.parent = tablero.transform;

        _logicMap.SpawnMinas();

        _logicMap.SpawnNums();
    }



}

