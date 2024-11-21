using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    public Vector3 pos;
    public LogicMap _logicMap;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(this.gameObject.transform.parent);
            if (_logicMap != null)
            {
                _logicMap.Click(this.gameObject);
            }
        }
    }
}
