using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMape : MonoBehaviour
{

    private bool rigtClickDown = false;
    public float speedRtotation;
    public float distance;
    [SerializeField] private LayerMask masck;

    private void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, distance, masck))
            {
                if (hit.collider != null)
                {
                    rigtClickDown = true;
                }
            }

        }

        if (Input.GetMouseButtonUp(1))
        {
            rigtClickDown = false;
        }

        if (rigtClickDown)
        {
            float x = Input.GetAxisRaw("Mouse X")  * speedRtotation;
            float y = Input.GetAxisRaw("Mouse Y") * speedRtotation;

            Quaternion rot = transform.rotation * Quaternion.Euler(-y, x, 0);

            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.1f);
        }
    }

}
