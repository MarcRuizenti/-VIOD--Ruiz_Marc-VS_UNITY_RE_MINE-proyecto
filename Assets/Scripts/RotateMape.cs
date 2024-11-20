using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMape : MonoBehaviour
{

    private bool rigtClickDown = false;
    public float speedRtotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            rigtClickDown = false;
        }

        if (rigtClickDown) { 
            Quaternion direction = Quaternion.LookRotation(transform.up, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, speedRtotation * Time.deltaTime);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)){
            rigtClickDown = true;
        }
        
    }

}
