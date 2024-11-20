using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMape : MonoBehaviour
{

    private bool rigtClickDown = false;
    public float speedRtotation;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            rigtClickDown = false;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)){
            rigtClickDown = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (rigtClickDown)
        {
            float x = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * speedRtotation;
            float y = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * speedRtotation;


            rb.AddTorque(Vector3.down * x);
            rb.AddTorque(Vector3.right * y);
        }
    }
}
