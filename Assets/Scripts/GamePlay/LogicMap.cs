using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicMap : MonoBehaviour
{
    public GameObject[,,] lista { get; private set; }
    private Material[,,] listaMeriales;
    private bool[,,] listaMinas;
    [SerializeField] private List<Material> numeroMinas;
    [SerializeField] private Material Mina;
    private int numCube = 11;
    private int numMinas = 99;

    private void OnEnable()
    {
        if (GameManager.Instance != null) numCube = GameManager.Instance.numCube;
        if (GameManager.Instance != null) numMinas = GameManager.Instance.numMinas;
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

    public void ClearArrays()
    {
        for (int y = 0; y < numCube; y++)
        {
            for (int x = 0; x < numCube; x++)
            {
                for (int z = 0; z < numCube; z++)
                {
                    if (lista[y, x, z])
                    {
                        Destroy(lista[y, x, z]);
                    }

                    if (listaMinas[y, x, z])
                    {
                        listaMinas[y, x, z] = false;
                    }
                }
            }
        }


        if (GameManager.Instance != null) numCube = GameManager.Instance.numCube;
        if (GameManager.Instance != null) numMinas = GameManager.Instance.numMinas;
        listaMinas = new bool[numCube, numCube, numCube];
        listaMeriales = new Material[numCube, numCube, numCube];
        lista = new GameObject[numCube, numCube, numCube];
    }

    public void SpawnMinas()
    {
        int x = 0, y = 0, z = 0;

        for (int i = 0; i < numMinas; i++)
        {
            bool isOk = false;

            do
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
                if (!listaMinas[y, x, z])
                {
                    listaMinas[y, x, z] = true;

                    listaMeriales[y, x, z] = Mina;
                    isOk = true;
                }
            } while (!isOk);
        }
    }

    public int numMatirial(GameObject cube)
    {
        int numMatirial = 0;

        Material matirial = cube.GetComponent<Renderer>().sharedMaterial;

        for (int i = 0; i < numeroMinas.Count; i++)
        {
            if (matirial == numeroMinas[i])
            {
                numMatirial = i; 
            }
        }

        return numMatirial;
    }
    public int numBanderasCube(GameObject cube)
    {
        int numBandera = 0;

        int y = (int)cube.GetComponent<CubeLogic>().pos.y;
        int x = (int)cube.GetComponent<CubeLogic>().pos.x;
        int z = (int)cube.GetComponent<CubeLogic>().pos.z;

        bool esEsquina = Esquina(y, x, z);

        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    int ny = y + dy;
                    int nx = x + dx;
                    int nz = z + dz;

                    if (dy == 0 && dx == 0 && dz == 0) continue;

                    if (ny < 0 || ny >= numCube || nx < 0 || nx >= numCube || nz < 0 || nz >= numCube) continue;

                    if (esEsquina || EsMismaCapa(y, x, z, ny, nx, nz))
                    {
                        if (lista[ny, nx, nz] != null && lista[ny, nx, nz].GetComponent<CubeLogic>() != null)
                        {
                            if (lista[ny, nx, nz].GetComponent<CubeLogic>().bandera)
                            {
                                numBandera++;
                            }
                        }
                    }
                }
            }
        }

        return numBandera;
    }
    public void dobelClick(GameObject cube)
    {
        int y = (int)cube.GetComponent<CubeLogic>().pos.y;
        int x = (int)cube.GetComponent<CubeLogic>().pos.x;
        int z = (int)cube.GetComponent<CubeLogic>().pos.z;

        if (!cube.GetComponent<CubeLogic>().IAmclick) return;

        Material temp = listaMeriales[y, x, z];

        if (temp == Mina)
        {
            if (GameManager.Instance != null) GameManager.Instance.Loss();
            return;
        }

        int numBandera = numBanderasCube(cube);
        int numMinas = numMatirial(cube);
        if (numMinas == numBandera)
        {
            bool esEsquina = Esquina(y, x, z);

            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        int ny = y + dy;
                        int nx = x + dx;
                        int nz = z + dz;

                        if (dy == 0 && dx == 0 && dz == 0) continue;

                        if (ny < 0 || ny >= numCube || nx < 0 || nx >= numCube || nz < 0 || nz >= numCube) continue;

                        if (esEsquina || EsMismaCapa(y, x, z, ny, nx, nz))
                        {
                            CubeLogic vecino = null;

                            if (lista[ny, nx, nz] != null && lista[ny, nx, nz].GetComponent<CubeLogic>() != null)
                            {
                                vecino = lista[ny, nx, nz].GetComponent<CubeLogic>();
                                
                                if (!vecino.click)
                                {
                                    vecino.click = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void Click(GameObject cube)
    {
        int y = (int)cube.GetComponent<CubeLogic>().pos.y;
        int x = (int)cube.GetComponent<CubeLogic>().pos.x;
        int z = (int)cube.GetComponent<CubeLogic>().pos.z;

        Material temp = listaMeriales[y, x, z];

        cube.GetComponent<Renderer>().materials = new Material[] { temp };

        if (temp == Mina)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Loss();
                return;
            }
            SceneManager.LoadScene("Menu");
            return;
        }

        if (temp != numeroMinas[0]) return;

        bool esEsquina = Esquina(y, x, z);

        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    int ny = y + dy;
                    int nx = x + dx;
                    int nz = z + dz;

                    if (dy == 0 && dx == 0 && dz == 0) continue;

                    if (ny < 0 || ny >= numCube || nx < 0 || nx >= numCube || nz < 0 || nz >= numCube) continue;

                    if (esEsquina || EsMismaCapa(y, x, z, ny, nx, nz))
                    {
                        CubeLogic vecino = null;

                        if (lista[ny, nx, nz] != null && lista[ny, nx, nz].GetComponent<CubeLogic>() != null)
                        {
                            vecino = lista[ny, nx, nz].GetComponent<CubeLogic>();
                            if (!vecino.click)
                            {
                                vecino.click = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public void SpawnNums()
    {
        for (int y = 0; y < numCube; y++)
        {
            for (int x = 0; x < numCube; x++)
            {
                for (int z = 0; z < numCube; z++)
                {
                    bool esExterno = esExterna(y, x, z);

                    bool esEsquina = Esquina(y, x, z);

                    if (!esExterno || listaMinas[y, x, z])
                        continue;

                    int numVecinosConMina = ContarVecinosConMinas(y, x, z, esEsquina);

                    if (numVecinosConMina >= 0 && numVecinosConMina < numeroMinas.Count)
                    {
                        listaMeriales[y, x, z] = numeroMinas[numVecinosConMina];
                    }
                }
            }
        }
    }

    public bool checkWin()
    {
        bool win = true;

        for (int y = 0; y < numCube; y++)
        {
            for (int x = 0; x < numCube; x++)
            {
                for (int z = 0; z < numCube; z++) 
                {
                    if (!esExterna(y, x, z)) continue;

                    if (listaMinas[y, x, z])
                    {
                        if (!lista[y, x, z].GetComponent<CubeLogic>().bandera)
                        {
                            win = false; break;
                        }
                    }
                }
            }
        }

        return win;

    }

    private int ContarVecinosConMinas(int y, int x, int z, bool esEsquina)
    {
        int numVecinosConMina = 0;

        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    int ny = y + dy;
                    int nx = x + dx;
                    int nz = z + dz;

                    if (dy == 0 && dx == 0 && dz == 0) continue;

                    if (ny >= 0 && ny < numCube && nx >= 0 && nx < numCube && nz >= 0 && nz < numCube)
                    {
                        if (esEsquina)
                        {
                            if (listaMinas[ny, nx, nz]) numVecinosConMina++;
                        }
                        else
                        {
                            if (EsMismaCapa(y, x, z, ny, nx, nz))
                            {
                                if (listaMinas[ny, nx, nz]) numVecinosConMina++;
                            }
                        }
                    }
                }
            }
        }

        return numVecinosConMina;
    }
    public bool esExterna(int y, int x, int z)
    {
        return y == 0 || y == numCube - 1 ||
               x == 0 || x == numCube - 1 ||
               z == 0 || z == numCube - 1;
    }
    public bool Esquina(int y, int x, int z)
    {
        return (y == 0 || y == numCube - 1) &&
               (x == 0 || x == numCube - 1) &&
               (z == 0 || z == numCube - 1);
    }
    private bool EsMismaCapa(int y, int x, int z, int ny, int nx, int nz)
    {
        return (y == 0 || y == numCube - 1) && ny == y || 
               (x == 0 || x == numCube - 1) && nx == x || 
               (z == 0 || z == numCube - 1) && nz == z;
    }
}
