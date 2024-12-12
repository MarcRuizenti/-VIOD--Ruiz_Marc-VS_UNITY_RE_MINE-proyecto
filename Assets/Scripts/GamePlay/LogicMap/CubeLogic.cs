using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [Header("Observ")]
    [SerializeField] private Material _defaultTransparent;
    [SerializeField] private GameObject _miniCubePrefab;
    private GameObject _miniCube;
    private bool IAmTransparent = false;

    [Header("Logic")]
    [SerializeField] private Material _bandera;
    [SerializeField] private Material Outline;
    private Material _default;
    public bool bandera = false;
    private bool posibleBandera = false;

    public Vector3 pos;
    public LogicMap _logicMap;
    public bool IAmclick = false;
    private bool iAmSelected = false;
    public bool callVecino = false;
    public int numCube;

    private GameObject[] vecinos;

    private void Start()
    {
        vecinos = _logicMap.GetVecions(this);
    }


    private void Update()
    {
        if (iAmSelected)
        {
            if (transform.gameObject.GetComponent<Renderer>().materials.Length == 1)
            {
                Material temp = transform.gameObject.GetComponent<Renderer>().material;
                transform.gameObject.GetComponent<Renderer>().materials = new Material[] { temp, Outline };

                if (!callVecino)
                {
                    if (GameManager.Instance != null)
                    {
                        if (GameManager.Instance.activeRevelar && GameManager.Instance.habilityActive.level == 2)
                        {
                            for (int i = 0; i < vecinos.Length; i++)
                            {
                                if (vecinos[i] != null)
                                {
                                    vecinos[i].GetComponent<CubeLogic>().iAmSelected = true;
                                    vecinos[i].GetComponent<CubeLogic>().callVecino = true;
                                }
                            }
                        }
                    }
                }
            }
            
        }
        else
        {
            if (transform.gameObject.GetComponent<Renderer>().materials.Length == 2)
            {
                Material temp = transform.gameObject.GetComponent<Renderer>().material;
                transform.gameObject.GetComponent<Renderer>().materials = new Material[] { temp };
                if (!callVecino)
                {
                    if (GameManager.Instance != null)
                    {
                        if ((GameManager.Instance.activeRevelar && GameManager.Instance.habilityActive.level == 2 )|| vecinos[0].GetComponent<Renderer>().materials.Length == 2)
                        {
                            for (int i = 0; i < vecinos.Length; i++)
                            {
                                if (vecinos[i] != null)
                                {
                                    vecinos[i].GetComponent<CubeLogic>().iAmSelected = false;
                                    vecinos[i].GetComponent<CubeLogic>().callVecino = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void OnMouseOver()
    {
        
        iAmSelected = true;

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.canMove)
            {
                DetectClick();
            }
        }
        else
        {
            DetectClick();
        }

    }

    private void OnMouseExit()
    {
        iAmSelected = false;
    }

    private void DetectClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_logicMap != null)
            {
                if (IAmclick)
                {
                    _logicMap.dobelClick(this.gameObject);

                }
                if (!IAmclick && !bandera)
                {
                    if (GameManager.Instance != null)
                    {
                        if (GameManager.Instance.activeRevelar && !IAmTransparent)
                        {
                            Transparent();

                            if (GameManager.Instance.habilityActive.level == 2)
                            {
                                for (int i = 0; i < vecinos.Length; i++)
                                {
                                    if (vecinos[i] != null)
                                    {
                                        vecinos[i].GetComponent<CubeLogic>().Transparent();
                                    }
                                }
                            }

                            GameManager.Instance.activeRevelar = false;
                            GameManager.Instance.DesactiveAbility();
                            return;
                        }
                    }
                    IAmclick = true;
                    _logicMap.Click(this.gameObject);
                    if (GameManager.Instance != null)
                    {
                        if (GameManager.Instance.activeSandClock)
                        {
                            GameManager.Instance.activeSandClock = false;
                            GameManager.Instance.DesactiveAbility();
                        }
                        else if (GameManager.Instance.activeShild) 
                        {
                            GameManager.Instance.activeShild = false;
                            GameManager.Instance.DesactiveAbility();
                        }
                    }
                    DestroyMiniCube();
                }

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            posibleBandera = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (posibleBandera && !IAmclick)
            {
                if (!bandera)
                {
                    _default = transform.GetComponent<Renderer>().material;
                    transform.GetComponent<Renderer>().material = _bandera;
                    bandera = !bandera;
                    if (GameManager.Instance != null) GameManager.Instance.AddBandera();
                }
                else
                {
                    transform.GetComponent<Renderer>().material = _default;
                    bandera = !bandera;
                    if (GameManager.Instance != null) GameManager.Instance.RemoveBandera();
                }

                posibleBandera = false;
            }
        }
    }

    private void Transparent()
    {
        transform.GetComponent<Renderer>().material = _defaultTransparent;
        GameObject temp = Instantiate(_miniCubePrefab, transform.position, transform.rotation);

        temp.GetComponent<Renderer>().material = _logicMap.GiveMatirial(this.gameObject);

        _miniCube = temp;

        IAmTransparent = true;
    }

    public void DestroyMiniCube()
    {
        if (IAmTransparent)
        {
            Destroy(_miniCube);
        }
    }
    
}
