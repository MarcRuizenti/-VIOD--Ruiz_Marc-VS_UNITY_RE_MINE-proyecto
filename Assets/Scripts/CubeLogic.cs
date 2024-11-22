using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    public Vector3 pos;
    public LogicMap _logicMap;
    public bool click = false;
    public bool IAmclick = false;

    private void Update()
    {
        if (click && !IAmclick) 
        {
            if (_logicMap != null)
            {
                _logicMap.Click(this.gameObject);
                click = false;
                IAmclick = true;
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_logicMap != null)
            {
                if (!IAmclick) 
                { 
                    _logicMap.Click(this.gameObject);
                    IAmclick = true;
                }
            }
        }
    }
}
