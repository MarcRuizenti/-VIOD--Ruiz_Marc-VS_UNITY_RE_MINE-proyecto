using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
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
                        if (GameManager.Instance.activeRevelar)
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
                        if (GameManager.Instance.activeRevelar)
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

                    IAmclick = true;
                    _logicMap.Click(this.gameObject);
                    if (GameManager.Instance != null)
                    {
                        if (GameManager.Instance.activeSandClock)
                        {
                            GameManager.Instance.activeSandClock = false;
                            GameManager.Instance.oneHanilityActive = false;
                            GameManager.Instance.habilityActive.energy--;
                            GameManager.Instance.habilityActive = null;
                        }
                    }
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
}
