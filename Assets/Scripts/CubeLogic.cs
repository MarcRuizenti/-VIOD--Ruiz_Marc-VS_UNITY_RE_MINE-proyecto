using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private Material _bandera;
    [SerializeField] private Material _default;
    private bool bandera = false;
    private bool posibleBandera = false;

    public Vector3 pos;
    public LogicMap _logicMap;
    public bool click = false;
    public bool IAmclick = false;

    private void Update()
    {
        if (click && !IAmclick && !bandera) 
        {
            if (_logicMap != null)
            {
                _logicMap.Click(this.gameObject);
                click = false;
                IAmclick = true;
            }
        }
        click = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_logicMap != null)
            {
                if (!IAmclick && !bandera) 
                { 
                    _logicMap.Click(this.gameObject);
                    IAmclick = true;
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            posibleBandera = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (posibleBandera)
            {
                if (!bandera)
                {
                    transform.GetComponent<Renderer>().material = _bandera;
                    bandera = !bandera;
                }
                else
                {
                    transform.GetComponent<Renderer>().material = _default;
                    bandera = !bandera;
                }

                posibleBandera = false;
            }
        }
    }
}
