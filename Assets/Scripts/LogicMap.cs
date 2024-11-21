using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicMap : MonoBehaviour
{
    private GameObject[,,] lista;
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
            // Elegir aleatoriamente una cara externa
            int cara = Random.Range(0, 6); // Hay 6 caras en un cubo

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
            lista[y, x, z].GetComponent<Renderer>().material = Mina;
            listaMinas[y, x, z] = true;
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
                    // Comprobar si está en una cara externa
                    bool esExterno = y == 0 || y == numCube - 1 ||  // Cara superior/inferior
                                     x == 0 || x == numCube - 1 ||  // Cara izquierda/derecha
                                     z == 0 || z == numCube - 1;

                    // Comprobar si es una esquina
                    bool esEsquina = (y == 0 || y == numCube - 1) &&
                                     (x == 0 || x == numCube - 1) &&
                                     (z == 0 || z == numCube - 1);

                    // Si no es externo o si ya tiene el material Mina, ignorar
                    if (!esExterno || listaMinas[y, x, z])
                        continue;

                    int numVecinosConMina = 0;

                    if (esEsquina)
                    {
                        // Para esquinas, evaluar todos los vecinos alrededor de la casilla
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

                                    // Verificar si el vecino está dentro de los límites
                                    if (ny >= 0 && ny < numCube && nx >= 0 && nx < numCube && nz >= 0 && nz < numCube)
                                    {
                                        // Contar si el vecino tiene el material Mina
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
                        // Para caras externas que no son esquinas, evaluar vecinos en la misma capa
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

                                    // Verificar si el vecino está dentro de los límites
                                    if (ny >= 0 && ny < numCube && nx >= 0 && nx < numCube && nz >= 0 && nz < numCube)
                                    {
                                        // Comprobar si el vecino está en la misma capa
                                        if ((y == 0 || y == numCube - 1) && ny == y) // Capa superior/inferior
                                        {
                                            if (listaMinas[ny, nx, nz])
                                                numVecinosConMina++;
                                        }
                                        else if ((x == 0 || x == numCube - 1) && nx == x) // Capa izquierda/derecha
                                        {
                                            if (listaMinas[ny, nx, nz])
                                                numVecinosConMina++;
                                        }
                                        else if ((z == 0 || z == numCube - 1) && nz == z) // Capa frontal/trasera
                                        {
                                            if (listaMinas[ny, nx, nz])
                                                numVecinosConMina++;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Cambiar el material según el número de vecinos con Mina
                    if (numVecinosConMina > 0 && numVecinosConMina < numeroMinas.Count)
                    {
                        lista[y, x, z].GetComponent<Renderer>().material = numeroMinas[numVecinosConMina - 1];
                    }
                }
            }
        }
    }
}
