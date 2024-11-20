using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTablero : MonoBehaviour
{
    [SerializeField] private GameObject tablero;
    [SerializeField] private GameObject cube;
    [SerializeField] private Camera _camera;
    public int numCube;



    // Start is called before the first frame update
    void Start()
    {
        int centro = numCube / 2;
        tablero.transform.GetComponent<BoxCollider>().size = new Vector3(numCube, numCube, numCube);
        tablero.transform.position = new Vector3(centro, centro, centro);

        for (int i = 0; i < numCube; i++)
        {
            for (int j = 0; j < numCube; j++)
            {
                for (int k = 0; k < numCube; k++)
                {
                    // Posiciona cada cubo en el espacio
                    GameObject temp = Instantiate(cube);
                    
                    temp.transform.position = new Vector3(j, i, k);
                    temp.transform.parent = tablero.transform;

                }
            }
        }
        
        
        _camera.transform.position += new Vector3(centro, centro, centro);
        _camera.transform.position += -_camera.transform.forward * numCube;
        _camera.transform.position += -transform.up * centro * .5f;

    }



}

