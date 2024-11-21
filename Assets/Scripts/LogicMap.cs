using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicMap : MonoBehaviour
{
    public GameObject[,,] lista { get; private set; }
    private Material[,,] listaMeriales;
    private bool[,,] listaMinas;
    [SerializeField] private List<Material> numeroMinas;
    [SerializeField] private Material Mina;
    private int numCube;
    private int numMinas;
    private void OnEnable()
    {
        numCube = GameManager.Instance.numCube;
        numMinas = GameManager.Instance.numMinas;
        listaMinas = new bool[numCube, numCube, numCube];
        listaMeriales = new Material[numCube, numCube, numCube];
    }


    public void AddCube(GameObject cube, int y, int x, int z)
    {
        if (cube != null)
        {
            if (lista != null)
            {
                lista[y, x, z] = cube;
            }
            else
            {
                lista = new GameObject[numCube, numCube, numCube];
                lista[y, x, z] = cube;
            }
        }

    }

    public void SpawnMinas()
    {
        int x = 0, y = 0, z = 0;

        for (int i = 0; i < numMinas; i++)
        {
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
            listaMinas[y, x, z] = true;

            listaMeriales[y, x, z] = Mina;

        }
    }
    
    public void Click(GameObject cube)
    {
        Debug.Log("Click2");
        Material temp = listaMeriales[(int)cube.GetComponent<CubeLogic>().pos.y, (int)cube.GetComponent<CubeLogic>().pos.x, (int)cube.GetComponent<CubeLogic>().pos.z];
        cube.GetComponent<Renderer>().materials = new Material[] { temp };       
    }

    public void SpawnNums()
    {
        for (int y = 0; y < numCube; y++)
        {
            for (int x = 0; x < numCube; x++)
            {
                for (int z = 0; z < numCube; z++)
                {
                    bool esExterno = y == 0 || y == numCube - 1 ||  
                                     x == 0 || x == numCube - 1 ||  
                                     z == 0 || z == numCube - 1;

                    bool esEsquina = (y == 0 || y == numCube - 1) &&
                                     (x == 0 || x == numCube - 1) &&
                                     (z == 0 || z == numCube - 1);

                    if (!esExterno || listaMinas[y, x, z])
                        continue;

                    int numVecinosConMina = 0;

                    if (esEsquina)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            for (int dx = -1; dx <= 1; dx++)
                            {
                                for (int dz = -1; dz <= 1; dz++)
                                {
                                    int ny = y + dy;
                                    int nx = x + dx;
                                    int nz = z + dz;

                                    if (dy == 0 && dx == 0 && dz == 0)
                                        continue;

                                    if (ny >= 0 && ny < numCube && nx >= 0 && nx < numCube && nz >= 0 && nz < numCube)
                                    {
                                        if (listaMinas[ny, nx, nz])
                                        {
                                            numVecinosConMina++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            for (int dx = -1; dx <= 1; dx++)
                            {
                                for (int dz = -1; dz <= 1; dz++)
                                {
                                    int ny = y + dy;
                                    int nx = x + dx;
                                    int nz = z + dz;

                                    if (dy == 0 && dx == 0 && dz == 0)
                                        continue;

                                    if (ny >= 0 && ny < numCube && nx >= 0 && nx < numCube && nz >= 0 && nz < numCube)
                                    {
                                        if ((y == 0 || y == numCube - 1) && ny == y) 
                                        {
                                            if (listaMinas[ny, nx, nz])
                                                numVecinosConMina++;
                                        }
                                        else if ((x == 0 || x == numCube - 1) && nx == x) 
                                        {
                                            if (listaMinas[ny, nx, nz])
                                                numVecinosConMina++;
                                        }
                                        else if ((z == 0 || z == numCube - 1) && nz == z) 
                                        {
                                            if (listaMinas[ny, nx, nz])
                                                numVecinosConMina++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (numVecinosConMina >= 0 && numVecinosConMina < numeroMinas.Count)
                    {
                        listaMeriales[y, x, z] = numeroMinas[numVecinosConMina];
                    }
                }
            }
        }
    }
}
