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
        int y = (int)cube.GetComponent<CubeLogic>().pos.y;
        int x = (int)cube.GetComponent<CubeLogic>().pos.x;
        int z = (int)cube.GetComponent<CubeLogic>().pos.z;

        Material temp = listaMeriales[y, x, z];
        cube.GetComponent<Renderer>().materials = new Material[] { temp };

        if (temp != numeroMinas[0]) return;

        // Determinar si es una esquina
        bool esEsquina = (y == 0 || y == numCube - 1) &&
                         (x == 0 || x == numCube - 1) &&
                         (z == 0 || z == numCube - 1);

        // Determinar vecinos relevantes
        for (int dy = -1; dy <= 1; dy++)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dz = -1; dz <= 1; dz++)
                {
                    // Coordenadas del vecino
                    int ny = y + dy;
                    int nx = x + dx;
                    int nz = z + dz;

                    // Saltar si es la misma casilla
                    if (dy == 0 && dx == 0 && dz == 0)
                        continue;

                    // Verificar límites del array
                    if (ny < 0 || ny >= numCube || nx < 0 || nx >= numCube || nz < 0 || nz >= numCube)
                        continue;

                    // Verificar si el vecino está en la misma capa exterior
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

    // Verifica si el vecino está en la misma capa externa que el cubo actual
    private bool EsMismaCapa(int y, int x, int z, int ny, int nx, int nz)
    {
        return (y == 0 || y == numCube - 1) && ny == y || // Capa superior/inferior
               (x == 0 || x == numCube - 1) && nx == x || // Capa izquierda/derecha
               (z == 0 || z == numCube - 1) && nz == z;   // Capa frontal/trasera
    }


    public void SpawnNums()
    {
        for (int y = 0; y < numCube; y++)
        {
            for (int x = 0; x < numCube; x++)
            {
                for (int z = 0; z < numCube; z++)
                {
                    // Determinar si la casilla está en una cara externa
                    bool esExterno = y == 0 || y == numCube - 1 ||
                                     x == 0 || x == numCube - 1 ||
                                     z == 0 || z == numCube - 1;

                    // Determinar si la casilla está en una esquina
                    bool esEsquina = (y == 0 || y == numCube - 1) &&
                                     (x == 0 || x == numCube - 1) &&
                                     (z == 0 || z == numCube - 1);

                    // Ignorar casillas internas y aquellas que ya son minas
                    if (!esExterno || listaMinas[y, x, z])
                        continue;

                    // Contar vecinos con minas
                    int numVecinosConMina = ContarVecinosConMinas(y, x, z, esEsquina);

                    // Asignar material correspondiente según el número de minas vecinas
                    if (numVecinosConMina >= 0 && numVecinosConMina < numeroMinas.Count)
                    {
                        listaMeriales[y, x, z] = numeroMinas[numVecinosConMina];
                    }
                }
            }
        }
    }

    // Método para contar vecinos con minas
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

                    // Saltar si es la misma casilla
                    if (dy == 0 && dx == 0 && dz == 0)
                        continue;

                    // Verificar si el vecino está dentro de los límites del array
                    if (ny >= 0 && ny < numCube && nx >= 0 && nx < numCube && nz >= 0 && nz < numCube)
                    {
                        // En esquinas, considerar todos los vecinos
                        if (esEsquina)
                        {
                            if (listaMinas[ny, nx, nz])
                                numVecinosConMina++;
                        }
                        else
                        {
                            // Para caras externas, considerar solo vecinos en la misma capa
                            if ((y == 0 || y == numCube - 1) && ny == y ||
                                (x == 0 || x == numCube - 1) && nx == x ||
                                (z == 0 || z == numCube - 1) && nz == z)
                            {
                                if (listaMinas[ny, nx, nz])
                                    numVecinosConMina++;
                            }
                        }
                    }
                }
            }
        }

        return numVecinosConMina;
    }
}
