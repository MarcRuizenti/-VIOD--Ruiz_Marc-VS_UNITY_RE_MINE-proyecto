using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private Material _bandera;
    private Material _default;
    public bool bandera = false;
    private bool posibleBandera = false;

    public Vector3 pos;
    public LogicMap _logicMap;
    public bool IAmclick = false;

    private void OnMouseOver()
    {
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
                        GameManager.Instance.activeSandClock = false;
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
